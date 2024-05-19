// Validator Object
function Validator(options) {
    var selectorRules = {};

    function validate(inputElement, rule) {
        var errorElement = inputElement.parentElement.querySelector(options.errorSelector);
        var errorMessage;

        // Get the rules for the selector
        var rules = selectorRules[rule.selector];

        // Iterate through each rule and check for errors
        for (var i = 0; i < rules.length; ++i) {
            errorMessage = rules[i](inputElement.value);
            if (errorMessage) break;
        }

        if (errorMessage) {
            errorElement.innerText = errorMessage;
            inputElement.parentElement.classList.add("invalid");
        } else {
            errorElement.innerText = "";
            inputElement.parentElement.classList.remove("invalid");
        }
    }

    // Get the form element to validate
    var formElement = document.querySelector(options.form);
    if (formElement) {
        options.rules.forEach(function (rule) {
            // Save the rules for each input
            if (Array.isArray(selectorRules[rule.selector])) {
                selectorRules[rule.selector].push(rule.test);
            } else {
                selectorRules[rule.selector] = [rule.test];
            }

            var inputElement = formElement.querySelector(rule.selector);

            if (inputElement) {
                // Handle blur event
                inputElement.onblur = function () {
                    validate(inputElement, rule);
                };

                // Handle input event
                inputElement.oninput = function () {
                    var errorElement = inputElement.parentElement.querySelector(options.errorSelector);
                    errorElement.innerText = "";
                    inputElement.parentElement.classList.remove("invalid");
                };
            }
        });
    }
}

// Define validation rules
Validator.isRequired = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            var regex = /^[A-Za-z0-9_.]+$/;
            return regex.test(value.trim()) ? undefined : message || "Vui lòng nhập đúng cú pháp!";
        }
    };
};

Validator.isNotEmpty = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            return value.trim() ? undefined : message || "Vui lòng không để trống";
        }
    };
};

Validator.isTextOnly = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            var regex = /^[A-Za-z ]+$/;
            return regex.test(value.trim()) ? undefined : message || "Vui lòng nhập đúng cú pháp!";
        }
    };
};

Validator.isNumberOnly = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            var regex = /^[0-9]+$/;
            return regex.test(value.trim()) ? undefined : message || "Vui lòng nhập đúng cú pháp!";
        }
    };
};

Validator.isEmail = function (selector, message) {
    return {
        selector: selector,
        test: function (value) {
            var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            return regex.test(value.trim()) ? undefined : message || "Trường này phải là email!";
        }
    };
};

Validator.minLength = function (selector, min, message) {
    return {
        selector: selector,
        test: function (value) {
            return value.length >= min ? undefined : message || `Vui lòng nhập tối thiểu ${min} kí tự`;
        }
    };
};

Validator.isConfirmed = function (selector, getConfirmValue, message) {
    return {
        selector: selector,
        test: function (value) {
            return value === getConfirmValue() ? undefined : message || "Giá trị nhập vào không chính xác";
        }
    };
};

// Avatar script
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var fileurl = e.target.result;
            $(".profile-pic").attr("src", fileurl);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$(".file-upload").on("change", function () {
    readURL(this);
});

$(".upload-button").on("click", function () {
    $(".file-upload").click();
});

// Pagination script
function getPageList(totalPages, page, maxLength) {
    function range(start, end) {
        return Array.from(Array(end - start + 1), (_, i) => i + start);
    }

    var sideWidth = maxLength < 9 ? 1 : 2;
    var leftWidth = (maxLength - sideWidth * 2 - 3) >> 1;
    var rightWidth = (maxLength - sideWidth * 2 - 3) >> 1;

    if (totalPages <= maxLength) {
        return range(1, totalPages);
    }

    if (page <= maxLength - sideWidth - 1 - rightWidth) {
        return range(1, maxLength - sideWidth - 1).concat(0, range(totalPages - sideWidth + 1, totalPages));
    }

    if (page >= totalPages - sideWidth - 1 - rightWidth) {
        return range(1, sideWidth).concat(0, range(totalPages - sideWidth - 1 - rightWidth - leftWidth, totalPages));
    }

    return range(1, sideWidth).concat(0, range(page - leftWidth, page + rightWidth), 0, range(totalPages - sideWidth + 1, totalPages));
}

$(function () {
    var numberOfItems = $(".content__list, .club-item").length;
    var limitPerPage = 9;
    var totalPages = Math.ceil(numberOfItems / limitPerPage);
    var paginationSize = 7;
    var currentPage;

    function showPage(whichPage) {
        if (whichPage < 1 || whichPage > totalPages) return false;
        currentPage = whichPage;
        $(".content__list .club-item").hide().slice((currentPage - 1) * limitPerPage, currentPage * limitPerPage).show();
        $(".pagination li").slice(1, -1).remove();
        getPageList(totalPages, currentPage, paginationSize).forEach((item) => {
            $("<li>")
                .addClass("pages page-item")
                .addClass(item ? "current-page" : "dots")
                .toggleClass("active", item === currentPage)
                .append($("<a>").addClass("page-link").attr({ href: "javascript: void(0)" }).text(item || "..."))
                .insertBefore(".next-page");
        });

        $(".previous-page").toggleClass("disabled", currentPage === 1);
        $(".next-page").toggleClass("disabled", currentPage === totalPages);
        return true;
    }

    $(".pagination").append(
        $("<li>").addClass("page-item previous-page").append(
            $("<a>").addClass("page-link btn").attr({ href: "javascript: void(0)" }).append($("<i>").addClass("fa fa-angle-left"))
        ),
        $("<li>").addClass("page-item next-page").append(
            $("<a>").addClass("page-link btn").attr({ href: "javascript: void(0)" }).append($("<i>").addClass("fa fa-angle-right"))
        )
    );

    $(".content__list").show();
    showPage(1);

    $(document).on("click", ".pagination li.current-page:not(.active)", function () {
        return showPage(+$(this).text());
    });

    $(".next-page").on("click", function () {
        return showPage(currentPage + 1);
    });

    $(".previous-page").on("click", function () {
        return showPage(currentPage - 1);
    });
});

// Truncate content
$(document).ready(function () {
    (function () {
        var showChar = 0;
        var ellipsestext = "";

        $(".truncate-content").each(function () {
            var content = $(this).html();
            if (content.length > showChar) {
                var c = content.substr(0, showChar);
                var h = content;
                var html = '<div class="truncate-text" style="display:block">' + c +
                    '<span class="moreellipses">' + ellipsestext + '&nbsp;&nbsp;<a href="" class="moreless more">Xem thêm</a></span></div>' +
                    '<div class="truncate-text" style="display:none">' + h + '<a href="" class="moreless less">Thu gọn</a></div>';

                $(this).html(html);
            }
        });

        $(".moreless").click(function () {
            var thisEl = $(this);
            var cT = thisEl.closest(".truncate-text");
            var tX = ".truncate-text";

            if (thisEl.hasClass("less")) {
                cT.prev(tX).toggle();
                cT.slideToggle();
            } else {
                cT.toggle();
                cT.next(tX).fadeToggle();
            }
            return false;
        });
    })();
});

// Clickable row
$(document).ready(function () {
    $(".clickable").click(function () {
        window.location = $(this).data("href");
    });
});

// Price format
var vnd = Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
    useGrouping: true
});

function price_format() {
    $(".price-format").each(function () {
        var $price = $(this).data("price"),
            html = vnd.format($price);
        $(this).html(html);
    });
}

$(function () {
    price_format();
});


// App A setup
