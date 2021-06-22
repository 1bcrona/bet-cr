(function ($) {
    "use strict";

    $.fn.addModal = function (options) {
        let defaults = {
            modalContent: "",
            closeTimer: 0,
            modalState: "success",
            afterCloseAction: function () {
            }
        };
        options = $.extend({}, defaults, options);

        let div = this;
        let id = options.id ?? "responseModal";
        let existingModal = $(`#${id}`);
        $(existingModal).remove();

        var modal = ` <div class="modal fade" id="${id}" role="dialog">
            <div class="modal-dialog">
                <div class="card">
                    <div class="card-img">
                        <img class="img-fluid">
                    </div>
                    <div class="card-title">
                    </div>
                    <div class="card-text text-center">
                        <p> ${options.modalContent}</p>
                    </div>
                    <div class="progress">
                        <div class="progress-bar" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                            <span class="sr-only"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;

        $(div).append(modal);

        let currentModal = $(`#${id}`);
        if (options.closeTimer > 0) {
            $('.progress-bar').css('width', '0%');
            let i = 0;

            var counterBack = setInterval(function () {
                console.log(i);
                if (i <= options.closeTimer) {
                    $('.progress-bar').css('width', (i) * (100 / options.closeTimer) + '%');
                    i += 0.1;
                } else {
                    currentModal.modal('hide');
                    clearInterval(counterBack);
                }
            }, 100);
        }

        $(currentModal).on('hide.bs.modal',
            function (el, event) {
                options.afterCloseAction();
            });
        let hide = false;

        $(currentModal).on('show.bs.modal', function (el, event) {
            if (hide) {
                $(currentModal).modal('hide')
            }
        });

        $(currentModal).on('shown.bs.modal', function (el, event) {
            if (hide) {
                $(currentModal).modal('hide')
            }
        });

        let modalElement =
        {
            element: $(currentModal),
            show: function () {
                let img = $(currentModal).find(".img-fluid");
                let card = $(currentModal).find(".card");
                let pb = $(currentModal).find(".progress-bar");
                $(img).removeClass("img-success img-error img-loading");
                $(card).removeClass("card-success card-error card-loading");
                $(pb).removeClass("progress-bar-error progress-bar-success progress-bar-loading");
                if (options.modalState === "success") {
                    $(card).addClass("card-success");
                    $(img).addClass("img-success");
                    $(pb).addClass("progress-bar-success");
                } else if (options.modalState === "loading") {
                    $(card).addClass("card-loading");
                    $(img).addClass("img-loading");
                    $(pb).addClass("progress-bar-loading");
                }
                else {
                    $(card).addClass("card-error");
                    $(img).addClass("img-error");
                    $(pb).addClass("progress-bar-error");
                }

                $(currentModal).modal('show');
            },
            hide: function () {
                hide = true;
                $(currentModal).modal('show');
                $(currentModal).modal('hide');
            }
        }

        return {
            modalElement
        }
    };
})(jQuery);