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
                <div class="card modal-card">
                    <div class="card-img modal-card-img">
                        <img class="img-fluid">
                    </div>
                    <div class="modal-card-title card-title">
                    </div>
                    <div class="modal-card-text card-text text-center">
                        <p> ${options.modalContent}</p>
                    </div>
                    <div class="modal-progress progress">
                        <div class="progress-bar modal-progress-bar" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="width: 0%;">
                            <span class="sr-only"></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;

        $(div).append(modal);

        let currentModal = $(`#${id}`);
        if (options.closeTimer > 0) {
            $('.modal-progress-bar').css('width', '0%');
            let i = 0;

            var counterBack = setInterval(function () {
                console.log(i);
                if (i <= options.closeTimer) {
                    $('.modal-progress-bar').css('width', (i) * (100 / options.closeTimer) + '%');
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
                let card = $(currentModal).find(".modal-card");
                let pb = $(currentModal).find(".modal-progress-bar");
                $(img).removeClass("modal-img-success modal-img-error modal-img-loading");
                $(card).removeClass("modal-card-success modal-card-error modal-card-loading");
                $(pb).removeClass("modal-progress-bar-error modal-progress-bar-success modal-progress-bar-loading");
                if (options.modalState === "success") {
                    $(card).addClass("modal-card-success");
                    $(img).addClass("modal-img-success");
                    $(pb).addClass("modal-progress-bar-success");
                } else if (options.modalState === "loading") {
                    $(card).addClass("modal-card-loading");
                    $(img).addClass("modal-img-loading");
                    $(pb).addClass("modal-progress-bar-loading");
                }
                else {
                    $(card).addClass("modal-card-error");
                    $(img).addClass("modal-img-error");
                    $(pb).addClass("modal-progress-bar-error");
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