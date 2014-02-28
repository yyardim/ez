define('datacontext',
    ['jquery', 'underscore', 'ko', 'model', 'model.mapper', 'dataservice', 'config', 'utils', 'datacontext.speaker-sessions'],
    function ($, _, ko, model, modelmapper, dataservice, config, utils, SpeakerSessions) {
        var logger = config.logger,
            getCurrentUserId = function() {
                return config.currentUser().id();
            },
            itemsToArray = function(items, observableArray, filter, sortFunction) {
                // Maps the memo to an observableArray, 
                // then returns the observableArray
                if (!observableArray) return;

                // Create an array from the memo object
                var underlyingArray = utils.mapMemoToArray(items);

                if (filter) {
                    underlyingArray = _.filter(underlyingArray, function(o) {
                        var match = filter.predicate(filter, o);
                        return match;
                    });
                }
                if (sortFunction) {
                    underlyingArray.sort(sortFunction);
                }
                //logger.info('Fetched, filtered and sorted ' + underlyingArray.length + ' records');
                observableArray(underlyingArray);
            },
            mapToContext = function(dtoList, items, results, mapper, filter, sortFunction) {
                // Loop through the raw dto list and populate a dictionary of the items
                items = _.reduce(dtoList, function(memo, dto) {
                    var id = mapper.getDtoId(dto);
                    var existingItem = items[id];
                    memo[id] = mapper.fromDto(dto, existingItem);
                    return memo;
                }, {});
                itemsToArray(items, results, filter, sortFunction);
                //logger.success('received with ' + dtoList.length + ' elements');
                return items; // must return these
            },
            EntitySet = function(getFunction, mapper, nullo, updateFunction) {
                var items = {},
                    // returns the model item produced by merging dto into context
                    mapDtoToContext = function(dto) {
                        var id = mapper.getDtoId(dto);
                        var existingItem = items[id];
                        items[id] = mapper.fromDto(dto, existingItem);
                        return items[id];
                    },
                    add = function(newObj) {
                        items[newObj.id()] = newObj;
                    },
                    removeById = function(id) {
                        delete items[id];
                    },
                    getLocalById = function(id) {
                        // This is the only place we set to NULLO
                        return !!id && !!items[id] ? items[id] : nullo;
                    },
                    getAllLocal = function() {
                        return utils.mapMemoToArray(items);
                    },
                    getData = function(options) {
                        return $.Deferred(function(def) {
                            var results = options && options.results,
                                sortFunction = options && options.sortFunction,
                                filter = options && options.filter,
                                forceRefresh = options && options.forceRefresh,
                                param = options && options.param,
                                getFunctionOverride = options && options.getFunctionOverride;

                            getFunction = getFunctionOverride || getFunction;

                            // If the internal items object doesnt exist, 
                            // or it exists but has no properties, 
                            // or we force a refresh
                            if (forceRefresh || !items || !utils.hasProperties(items)) {
                                getFunction({
                                    success: function(dtoList) {
                                        items = mapToContext(dtoList, items, results, mapper, filter, sortFunction);
                                        def.resolve(results);
                                    },
                                    error: function(response) {
                                        logger.error(config.toasts.errorGettingData);
                                        def.reject();
                                    }
                                }, param);
                            } else {
                                itemsToArray(items, results, filter, sortFunction);
                                def.resolve(results);
                            }
                        }).promise();
                    },
                    updateData = function(entity, callbacks) {

                        var entityJson = ko.toJSON(entity);

                        return $.Deferred(function(def) {
                            if (!updateFunction) {
                                logger.error('updateData method not implemented');
                                if (callbacks && callbacks.error) {
                                    callbacks.error();
                                }
                                def.reject();
                                return;
                            }

                            updateFunction({
                                success: function(response) {
                                    logger.success(config.toasts.savedData);
                                    entity.dirtyFlag().reset();
                                    if (callbacks && callbacks.success) {
                                        callbacks.success();
                                    }
                                    def.resolve(response);
                                },
                                error: function(response) {
                                    logger.error(config.toasts.errorSavingData);
                                    if (callbacks && callbacks.error) {
                                        callbacks.error();
                                    }
                                    def.reject(response);
                                    return;
                                }
                            }, entityJson);
                        }).promise();
                    };

                return {
                    mapDtoToContext: mapDtoToContext,
                    add: add,
                    getAllLocal: getAllLocal,
                    getLocalById: getLocalById,
                    getData: getData,
                    removeById: removeById,
                    updateData: updateData
                };
            },

            //----------------------------------
            // Repositories
            //
            // Pass: 
            //  dataservice's 'get' method
            //  model mapper
            //----------------------------------
            //ezPersons = new EntitySet(dataservice.ezPersons.getEventz, modelmapper.ezPersons, model.ezPersons.Nullo);
            //addresses = new EntitySet(dataservice.lookup.getRooms, modelmapper.room, model.Room.Nullo),
            ezs = new EntitySet(dataservice.ez.getEzs, modelmapper.ez, model.Ez.Nullo),
            persons = new EntitySet(dataservice.person.getPersons, modelmapper.person, model.Person.Nullo);
            //timeslots = new EntitySet(dataservice.lookup.getTimeslots, modelmapper.timeSlot, model.TimeSlot.Nullo),
            //tracks = new EntitySet(dataservice.lookup.getTracks, modelmapper.track, model.Track.Nullo),
            //speakerSessions = new SpeakerSessions.SpeakerSessions(persons, sessions);
        {   // yy - I put that curly brace to be able to collapse the shit below
        // Attendance extensions
        //ezPersons.addData = function (ezPersonsModel, callbacks) {
        //    var attendanceModel = new model.Attendance()
        //            .sessionId(sessionModel.id())
        //            .personId(getCurrentUserId()),
        //            attendanceModelJson = ko.toJSON(attendanceModel);

        //    return $.Deferred(function (def) {
        //        dataservice.attendance.addAttendance({
        //            success: function (dto) {
        //                if (!dto) {
        //                    logger.error(config.toasts.errorSavingData);
        //                    if (callbacks && callbacks.error) { callbacks.error(); }
        //                    def.reject();
        //                    return;
        //                }
        //                var newAtt = modelmapper.attendance.fromDto(dto); // Map DTO to Model
        //                attendance.add(newAtt); // Add to the datacontext
        //                sessionModel.isFavoriteRefresh.valueHasMutated(); // Trigger re-evaluation of isFavorite
        //                logger.success(config.toasts.savedData);
        //                if (callbacks && callbacks.success) { callbacks.success(newAtt); }
        //                def.resolve(dto);
        //            },
        //            error: function (response) {
        //                logger.error(config.toasts.errorSavingData);
        //                if (callbacks && callbacks.error) { callbacks.error(); }
        //                def.reject(response);
        //                return;
        //            }
        //        }, attendanceModelJson);
        //    }).promise();
        //};

        //attendance.updateData = function (sessionModel, callbacks) {
        //    var
        //        attendanceModel = sessionModel.attendance(),
        //        attendanceModelJson = ko.toJSON(attendanceModel);

        //    return $.Deferred(function (def) {
        //        dataservice.attendance.updateAttendance({
        //            success: function (response) {
        //                logger.success(config.toasts.savedData);
        //                attendanceModel.dirtyFlag().reset();
        //                if (callbacks && callbacks.success) { callbacks.success(); }
        //                def.resolve(response);
        //            },
        //            error: function (response) {
        //                logger.error(config.toasts.errorSavingData);
        //                if (callbacks && callbacks.error) { callbacks.error(); }
        //                def.reject(response);
        //                return;
        //            }
        //        }, attendanceModelJson);
        //    }).promise();
        //};

        //attendance.deleteData = function (sessionModel, callbacks) {
        //    var attendanceModel = sessionModel.attendance();
        //    return $.Deferred(function (def) {
        //        dataservice.attendance.deleteAttendance({
        //            success: function (response) {
        //                attendance.removeById(attendanceModel.id());
        //                sessionModel.isFavoriteRefresh.valueHasMutated(); // Trigger re-evaluation of isFavorite
        //                logger.success(config.toasts.savedData);
        //                if (callbacks && callbacks.success) { callbacks.success(); }
        //                def.resolve(response);
        //            },
        //            error: function (response) {
        //                logger.error(config.toasts.errorSavingData);
        //                if (callbacks && callbacks.error) { callbacks.error(); }
        //                def.reject(response);
        //                return;
        //            }
        //        }, attendanceModel.personId(), attendanceModel.sessionId());
        //    }).promise();
        //};

        //// Extend Attendance entityset with ability to get attendance for the current user (aka, the favorite)
        //// This is a "Local" method, so it gets it from the DC only, no promise returned, no callbacks.
        //attendance.getLocalSessionFavorite = function (sessionId) {
        //    var
        //        id = model.Attendance.makeId(getCurrentUserId(), sessionId),
        //        att = attendance.getLocalById(id);
        //    return att;
        //};

        //// Extend Attendance entityset with ability to get attendance for the current user (aka, the favorite)
        //attendance.getSessionFavorite = function (sessionId, callbacks, forceRefresh) {
        //    return $.Deferred(function (def) {
        //        var
        //            id = model.Attendance.makeId(getCurrentUserId(), sessionId),
        //            att = attendance.getLocalById(id);

        //        if (att.isNullo || forceRefresh) {
        //            // get fresh from database:YY - 12082013
        //            dataservice.ezPersons.getEventz(
        //                {
        //                    success: function (dto) {
        //                        // updates the session returned from getLocalById() above
        //                        att = ezPersons.mapDtoToContext(dto);
        //                        if (callbacks && callbacks.success) { callbacks.success(att); }
        //                        def.resolve(dto);
        //                    },
        //                    error: function (response) {
        //                        logger.error('oops! could not retrieve ezPersons ' + sessionId);
        //                        if (callbacks && callbacks.error) { callbacks.error(response); }
        //                        def.reject(response);
        //                    }
        //                },
        //                getCurrentUserId(),
        //                sessionId
        //            );
        //        } else {
        //            if (callbacks && callbacks.success) { callbacks.success(att); }
        //            def.resolve(att);
        //        }
        //    }).promise();
        //};
        }
        // extend ezs enttityset 
        ezs.getFullEventById = function (id, callbacks, forceRefresh) {
            return $.Deferred(function (def) {
                var event = ezs.getLocalById(id);
                if (event.isNullo || event.isBrief() || forceRefresh) {
                    // if nullo or brief, get fresh from database
                    dataservice.event.getEvent({
                        success: function (dto) {
                            // updates the session returned from getLocalById() above
                            event = ezs.mapDtoToContext(dto);
                            event.isBrief(false); // now a full session
                            if (callbacks && callbacks.success) { callbacks.success(event); }
                            def.resolve(dto);
                        },
                        error: function (response) {
                            logger.error('oops! could not retrieve session ' + id);
                            if (callbacks && callbacks.error) { callbacks.error(response); }
                            def.reject(response);
                        }
                    },
                    id);
                }
                else {
                    if (callbacks && callbacks.success) { callbacks.success(event); }
                    def.resolve(event);
                }
            }).promise();
        };

        persons.getFullPersonById = function(id, callbacks, forceRefresh) {
            return $.Deferred(function(def) {
                var person = persons.getLocalById(id);
                if (person.isNullo || person.isBrief() || forceRefresh) {
                    // if nullo or brief, get fresh from database
                    dataservice.person.getPerson({
                            success: function(dto) {
                                // updates the person returned from getLocalById() above
                                person = persons.mapDtoToContext(dto);
                                person.isBrief(false); // now a full person
                                callbacks.success(person);
                                def.resolve(dto);
                            },
                            error: function(response) {
                                logger.error('oops! could not retrieve person ' + id);
                                if (callbacks && callbacks.error) {
                                    callbacks.error(response);
                                }
                                def.reject(response);
                            }
                        },
                        id);
                } else {
                    callbacks.success(person);
                    def.resolve(person);
                }
            }).promise();
        };

        //// Get the sessions in cache for which this person is 
        //// a speaker from local data (no 'promise')
        //persons.getLocalSpeakerSessions = function (personId) {
        //    return speakerSessions.getLocalSessionsBySpeakerId(personId);
        //};

        var datacontext = {
            //attendance: attendance,
            persons: persons,
            //rooms: rooms,
            ezs: ezs
            //speakerSessions: speakerSessions,
            //timeslots: timeslots,
            //tracks: tracks
        };

        // We did this so we can access the datacontext during its construction
        model.setDataContext(datacontext);

        return datacontext;
    });