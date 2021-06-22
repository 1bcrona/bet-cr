(function ($) {
    //$(document).on('click',
    //    '.match_result',
    //    function () {


    //        $("body").addResultItem();

    //        let dataJson = $(this).attr("data-match-result");
    //        let data = JSON.parse(dataJson);

    //        $("#imgIncidentHomeTeam").attr('src', `/static/images/team_logos/${data.HomeTeam.ExternalTeamId}.png`);
    //        $("#imgLineupHomeTeam").attr('src', `/static/images/team_logos/${data.HomeTeam.ExternalTeamId}.png`);
    //        $("#lblIncidentHomeTeam").html(data.HomeTeam.Id);
    //        $("#lblIncidentHomeTeamScore").html(data.MatchScore.HomeTeamScore);
    //        $("#imgIncidentAwayTeam").attr('src', `/static/images/team_logos/${data.AwayTeam.ExternalTeamId}.png`);
    //        $("#imgLineupAwayTeam").attr('src', `/static/images/team_logos/${data.AwayTeam.ExternalTeamId}.png`);
    //        $("#lblIncidentAwayTeam").html(data.AwayTeam.Id);
    //        $("#lblIncidentAwayTeamScore").html(data.MatchScore.AwayTeamScore);
    //        var incidents = data.MatchScore.Incidents;
    //        var lineups = data.MatchScore.Lineups;
    //        $("#divMatchIncidentsHome").empty();
    //        $("#divMatchIncidentsAway").empty();
    //        var lineupObject = JSON.parse(lineups);
    //        if (incidents != null) {
    //            var incidentObject = JSON.parse(incidents);
    //            var homeIncidents = incidentObject.filter(function (k, v) {
    //                return k.number == 1;
    //            });
    //            var awayIncidents = incidentObject.filter(function (k, v) {
    //                return k.number == 2;
    //            });
    //            //homeIncidents.sort(sort('elapsed'));
    //            //awayIncidents.sort(sort('elapsed'));

    //            $("#divMatchIncidentsHome").append('<div class="div-goal-incident"></div>');
    //            $("#divMatchIncidentsHome").append('<div class="div-card-incident"></div>');
    //            $("#divMatchIncidentsAway").append('<div class="div-goal-incident"></div>');
    //            $("#divMatchIncidentsAway").append('<div class="div-card-incident"></div>');

    //            $.each(homeIncidents,
    //                function (i, j) {
    //                    var incident = j;
    //                    if (incident.EventTypeText.trim().length > 0) {
    //                        if (incident.incident_typeFK === 7 || incident.incident_typeFK === 8) {
    //                            var div = $("#divMatchIncidentsHome").find(".div-goal-incident");
    //                            var incidentRow =
    //                                $('<div class="row" style="float:right!important;clear:both">');
    //                            var scorerLabel = $('<label>');
    //                            var minuteLabel = $('<label>');
    //                            $(scorerLabel).html(incident.name);
    //                            $(minuteLabel).html(incident.elapsed + "'");
    //                            $(incidentRow).append(scorerLabel);

    //                            $(incidentRow).append(" ");
    //                            $(incidentRow).append(minuteLabel);

    //                            if (incident.incident_typeFK === 7) {
    //                                $(incidentRow)
    //                                    .append(
    //                                        "<img class='imgResizeSmall' src='/static/images/goal.png' data-toggle='tooltip' alt='Goal' title='Goal'></img>");
    //                            } else if (incident.incident_typeFK === 8) {
    //                                $(incidentRow)
    //                                    .append(
    //                                        "<img class='imgResizeSmall' src='/static/images/penalty.png' data-toggle='tooltip' alt='Penalty Kick' title='Penalty Kick'></img>");
    //                            }

    //                            $(div).append(incidentRow);
    //                        } else if (incident.incident_typeFK === 10) {
    //                            var div = $("#divMatchIncidentsHome").find(".div-goal-incident");
    //                            var incidentRow =
    //                                $('<div class="row" style="float:right!important;clear:both">');
    //                            var scorerLabel = $('<label>');
    //                            var minuteLabel = $('<label>');
    //                            $(scorerLabel).html(incident.name);
    //                            $(minuteLabel).html(incident.elapsed + "'");
    //                            $(incidentRow).append(scorerLabel);
    //                            $(incidentRow).append(" ");
    //                            $(incidentRow).append(minuteLabel);
    //                            $(incidentRow)
    //                                .append(
    //                                    "<img class='imgResizeSmall' src='/static/images/own_goal.png' data-toggle='tooltip' alt='Own Goal' title='Own Goal'></img>");
    //                            $(div).append(incidentRow);
    //                        } else if (incident.incident_typeFK === 16 || incident.incident_typeFK === 15) {
    //                            var div = $("#divMatchIncidentsHome").find(".div-card-incident");
    //                            var incidentRow =
    //                                $('<div class="row" style="float:right!important;clear:both">');
    //                            var scorerLabel = $('<label>');
    //                            var minuteLabel = $('<label>');
    //                            $(scorerLabel).html(incident.name);
    //                            $(minuteLabel).html(incident.elapsed + "'");
    //                            $(incidentRow).append(scorerLabel);
    //                            $(incidentRow).append(" ");
    //                            $(incidentRow).append(minuteLabel);
    //                            $(incidentRow)
    //                                .append(
    //                                    "<img class='imgResizeSmall' src='/static/images/red_card.png' data-toggle='tooltip' alt='Red Card' title='Red Card'></img>");
    //                            $(div).append(incidentRow);
    //                        }
    //                    }
    //                });
    //            $.each(awayIncidents,
    //                function (i, j) {
    //                    var incident = j;
    //                    if (incident.EventTypeText.trim().length > 0) {
    //                        if (incident.incident_typeFK === 7 || incident.incident_typeFK === 8) {
    //                            var div = $("#divMatchIncidentsAway").find(".div-goal-incident");
    //                            var incidentRow =
    //                                $('<div class="row" style="float:left!important;clear:both">');
    //                            var scorerLabel = $('<label>');
    //                            var minuteLabel = $('<label>');
    //                            $(scorerLabel).html(incident.name);
    //                            $(minuteLabel).html(incident.elapsed + "'");
    //                            $(incidentRow).append(scorerLabel);


    //                            $(incidentRow).append(" ");
    //                            $(incidentRow).append(minuteLabel);
    //                            if (incident.incident_typeFK === 7) {
    //                                $(incidentRow)
    //                                    .append(
    //                                        "<img class='imgResizeSmall' src='/static/images/goal.png' data-toggle='tooltip' alt='Goal' title='Goal'></img>");
    //                            } else if (incident.incident_typeFK === 8) {
    //                                $(incidentRow)
    //                                    .append(
    //                                        "<img class='imgResizeSmall' src='/static/images/penalty.png' data-toggle='tooltip' alt='Penalty Kick' title='Penalty Kick'></img>");
    //                            }

    //                            $(div).append(incidentRow);
    //                        } else if (incident.incident_typeFK === 10) {
    //                            var div = $("#divMatchIncidentsAway").find(".div-goal-incident");
    //                            var incidentRow =
    //                                $('<div class="row" style="float:left!important;clear:both">');
    //                            var scorerLabel = $('<label>');
    //                            var minuteLabel = $('<label>');
    //                            $(scorerLabel).html(incident.name);
    //                            $(minuteLabel).html(incident.elapsed + "'");
    //                            $(incidentRow).append(scorerLabel);
    //                            $(incidentRow).append(" ");
    //                            $(incidentRow).append(minuteLabel);
    //                            $(incidentRow)
    //                                .append(
    //                                    "<img class='imgResizeSmall' src='/static/images/own_goal.png' data-toggle='tooltip' alt='Own Goal'  title='Own Goal' ></img>");
    //                            $(div).append(incidentRow);
    //                        } else if (incident.incident_typeFK === 16 || incident.incident_typeFK === 15) {
    //                            var div = $("#divMatchIncidentsAway").find(".div-card-incident");
    //                            var incidentRow =
    //                                $('<div class="row" style="float:left!important;clear:both">');
    //                            var scorerLabel = $('<label>');
    //                            var minuteLabel = $('<label>');
    //                            $(scorerLabel).html(incident.name);
    //                            $(minuteLabel).html(incident.elapsed + "'");
    //                            $(incidentRow).append(scorerLabel);
    //                            $(incidentRow).append(" ");
    //                            $(incidentRow).append(minuteLabel);
    //                            $(incidentRow)
    //                                .append(
    //                                    "<img class='imgResizeSmall' src='/static/images/red_card.png' data-toggle='tooltip' alt='Red Card' title='Red Card'></img>");
    //                            $(div).append(incidentRow);
    //                        }
    //                    }
    //                });
    //        }
    //        $("#divMatchIncidentsPanel").find('img').tooltip();
    //        $("#divMatchIncidentsPanel").data('lineup', lineupObject);
    //        $.fancybox({
    //            "minWidth": 1200,
    //            "autoScale": true,
    //            "autoDimensions": true,
    //            "minHeight": 900,
    //            closeBtn: true,
    //            helpers: {
    //                overlay: {
    //                    closeClick: false
    //                }
    //            },
    //            "beforeShow": function () {
    //                $("body").css({
    //                    'overflow-y': 'hidden'
    //                });
    //            },
    //            'beforeClose': function () {
    //                $("#divMatchIncidentsPanel").addClass("hidden");
    //                $("body").css({
    //                    'overflow-y': 'visible'
    //                });
    //            },
    //            "afterShow": function () {
    //                $("#divMatchIncidentsPanel").removeClass("hidden");
    //            },
    //            'content': $("#divMatchIncidentsPanel").show(),
    //            'afterClose': function () {
    //            }
    //        });
    //        if (lineupObject != null) {
    //            $("#divHomeTeamPlayers").empty();
    //            $("#divAwayTeamPlayers").empty();
    //            var firstTeam = lineupObject.filter(function (k, v) {
    //                return k.lineup_typeName != "Substitute player";
    //            });
    //            $.each(firstTeam,
    //                function (k, v) {
    //                    var item = v;
    //                    var itemRow = $('<div class="row">');
    //                    var numberCol = $('<div class="col-sm-2">');
    //                    var playerCol = $('<div class="col-sm-6">');
    //                    $(numberCol).append('<label>');
    //                    $(playerCol).append('<label>');
    //                    $(itemRow).append(numberCol);
    //                    $(itemRow).append(playerCol);
    //                    $(numberCol).find('label').css('text-align', 'left').html(item.shirt_number);
    //                    $(playerCol).find('label').css('text-align', 'left').html(item.name);
    //                    if (item.number === 1) $("#divHomeTeamPlayers").append(itemRow);
    //                    else if (item.number === 2) $("#divAwayTeamPlayers").append(itemRow);
    //                });
    //            $('.lineup').on('load',
    //                function () {
    //                    $(this).data('dominant-color', null);
    //                    $(this).data('dominant-color-2', null);
    //                    try {
    //                        var image = $(this)[0];
    //                        $(image).addClass("colorGet");
    //                        var colorThief = new ColorThief();
    //                        var color = colorThief.getColor(image, 5);
    //                        var palette = colorThief.getPalette(image, 5, 5);
    //                        var secondaryColor = palette[1];
    //                        $(image).removeClass("colorGet");
    //                        $(this).data('dominant-color', rgb2hex(color[0], color[1], color[2]));
    //                        $(this).data('dominant-color-2',
    //                            rgb2hex(secondaryColor[0], secondaryColor[1], secondaryColor[2]));
    //                    } catch (e) {
    //                    }
    //                });
    //        }
    //        $("#match-highlights").empty();
    //        if (data.HIGHLIGHTS != null) {
    //            $("#match-highlights")
    //                .append('<video controls><source src="' +
    //                    data.HIGHLIGHTS +
    //                    '" type="video/mp4" height="100%" width="100%">Your browser does not support the video tag.</video>');
    //        }
    //        var firstItem = $("#ulIncident").find("li")[0];
    //        $(firstItem).find('a').trigger("click");


    //    });

    //$(document).on('click', '#lineupItem', function (e) {
    //    var firstItem = $('.lineup')[0];
    //    $(firstItem).trigger('click');
    //});
    //$(document).on('click', '.lineup', function (e) {
    //    var lineup = $("#divMatchIncidentsPanel").data('lineup');
    //    var teamId = $(this).data("team-number");
    //    var image = $(this)[0];
    //    var dominantColor = $(image).data('dominant-color');
    //    var textColor = $(image).data('dominant-color-2');
    //    if (dominantColor != null) {
    //        createLineup(lineup, teamId, dominantColor, textColor);
    //    } else
    //        createLineup(lineup, teamId, null, null);
    //});

    //function createLineup(lineup, teamId, color, textColor) {
    //    var teamLineup = lineup.filter(function (k, v) {
    //        return k.number === teamId && k.lineup_typeName !== "Substitute player";
    //    });
    //    var data = [];
    //    var lineupCreator = new LineupCreator();
    //    var roles = ["GoalKeeper", "Defence", "Midfield", "Forward"];
    //    var lineupRoles = [];
    //    for (var r = 0; r < roles.length; r++) {
    //        var role = roles[r];
    //        var roleLineup = teamLineup.filter(function (k, v) {
    //            return k.lineup_typeName === role;
    //        });
    //        var length = roleLineup.length;
    //        if (role === "GoalKeeper") {
    //            lineupRoles = lineupRoles.concat(lineupCreator.get_keeper(length));
    //        }
    //        if (role === "Defence") {
    //            lineupRoles = lineupRoles.concat(lineupCreator.get_defence(length));
    //        }
    //        if (role === "Midfield") {
    //            if (length === 3) {
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_anchor(1));
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_midfield(2));
    //            } else if (length === 5) {
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_midfield(2));
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_attacking_midfield(3));
    //            } else if (length === 4) {
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_midfield(length));
    //            } else if (length === 6) {
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_anchor(2));
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_midfield(2));
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_attacking_midfield(2));
    //            }
    //        }
    //        if (role === "Forward") {
    //            if (length === 3) {
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_attacking_midfield(2));
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_forward(1));
    //            } else {
    //                lineupRoles = lineupRoles.concat(lineupCreator.get_forward(length));
    //            }
    //        }
    //    }
    //    for (var z = 0; z < teamLineup.length; z++) {
    //        var playerName = teamLineup[z].name;
    //        var pos = lineupRoles[z];
    //        var number = teamLineup[z].shirt_number;
    //        data.push({
    //            name: playerName,
    //            position: pos,
    //            number: number
    //        });
    //    }
    //    if (color == null)
    //        color = teamId === 1 ? "#428bca" : "#d9534f";
    //    var options = {
    //        field: {
    //            width: '100%!important',
    //            img: '/static/css/vendor/soccerfield/fifa_soccer_field_1.png',
    //            autoReveal: true
    //        },
    //        players: {
    //            font_size: 12,
    //            reveal: true,
    //            timeout: 1,
    //            sim: false,
    //            color: color,
    //            text_color: textColor
    //        }
    //    };
    //    $("#soccerfield").soccerfield(data, options);
    //}

})(jQuery);