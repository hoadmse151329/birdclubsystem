// ====================== Validation ====================
//Đối tượng
function Validator(options) {
  var selectorRules = {};

  function validate(inputElement, rule) {
    var errorElement = inputElement.parentElement.querySelector(
      options.errorSelector
    );
    var errorMessage;

    // Lấy ra các rules của selector
    var rules = selectorRules[rule.selector];

    // Lặp qua từng rule & kiểm tra
    // Nếu lỗi break
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
  // Lấy element của form cần validate
  var formElement = document.querySelector(options.form);
  if (formElement) {
    options.rules.forEach(function (rule) {
      // Lưu lại các rules cho mỗi input
      if (Array.isArray(selectorRules[rule.selector])) {
        selectorRules[rule.selector].push(rule.test);
      } else {
        selectorRules[rule.selector] = [rule.test];
      }
      var inputElement = formElement.querySelector(rule.selector);

      if (inputElement) {
        //xử lý trường hơp blur ra ngoài
        inputElement.onblur = function () {
          validate(inputElement, rule);
        };

        // xử lý khi người dùng nhập vào input
        inputElement.oninput = function () {
          var errorElement = inputElement.parentElement.querySelector(
            options.errorSelector
          );
          errorElement.innerText = "";
          inputElement.parentElement.classList.remove("invalid");
        };
      }
    });
  }
}

// Định nghĩa rules
// Nguyên tắc của các rules
// 1. Error -> msg báo lỗi
// 2. Hợp lệ -> không trả ra gì

Validator.isRequired = function (selector, message) {
  return {
    selector: selector,
    test: function (value) {
      var regex = /^[A-Za-z0-9_.]+$/;
      return regex.test(value.trim())
        ? undefined
        : message || "Vui lòng nhập đúng cú pháp!";
    },
  };
};

Validator.isNotEmpty = function (selector, message) {
  return {
    selector: selector,
    test: function (value) {
      return value.trim() ? undefined : message || "Vui lòng không để trống";
    },
  };
};

Validator.isTextOnly = function (selector, message) {
  return {
    selector: selector,
    test: function (value) {
      var regex = /^[A-Za-z ]+$/;
      return regex.test(value.trim())
        ? undefined
        : message || "Vui lòng nhập đúng cú pháp!";
    },
  };
};

Validator.isNumberOnly = function (selector, message) {
  return {
    selector: selector,
    test: function (value) {
      var regex = /^[0-9]+$/;
      return regex.test(value.trim())
        ? undefined
        : message || "Vui lòng nhập đúng cú pháp!";
    },
  };
};

Validator.isEmail = function (selector, message) {
  return {
    selector: selector,
    test: function (value) {
      var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
      return regex.test(value.trim())
        ? undefined
        : message || "Trường này phải là email!";
    },
  };
};

Validator.minLength = function (selector, min, message) {
  return {
    selector: selector,
    test: function (value) {
      return value.length >= min
        ? undefined
        : message || `Vui lòng nhập tối thiểu ${min} kí tự`;
    },
  };
};

Validator.isConfirmed = function (selector, getConfirmValue, message) {
  return {
    selector: selector,
    test: function (value) {
      return value === getConfirmValue()
        ? undefined
        : message || "Giá trị nhập vào không chính xác";
    },
  };
};

// ===================Thuy detail=====================

$(function () {
  app_a.setUp();
});
let vnd = Intl.NumberFormat("vi-VN", {
  style: "currency",
  currency: "VND",
  useGrouping: true,
});
var app_a = {
  tbl: "#app_a",
  h: "input.hour_val",
  r: "input.room_val",
  t: ".row_total",
  am: ".amount",
  p: ".price",
  setUp: function () {
    var _this = this;
    _this.amount();
    $(this.tbl)
      .find("input")
      .change(function () {
        _this.amount();
      });
  },
  amount: function () {
    var _this = this,
      am = $(_this.am),
      amount = 0;
    $(this.tbl)
      .find("tbody tr")
      .each(function () {
        amount += _this.rowtotal(this);
      });
    am.html(_this.toCur(amount));
  },
  rowtotal: function (row) {
    var _this = this,
      r = $(row),
      h = r.find(_this.h),
      p = r.find(_this.p),
      rm = r.find(_this.r),
      h_val = h.val(),
      r_val = rm.val(),
      p_val = p.val(),
      t = r.find(_this.t),
      total = h_val * r_val * p_val;
    t.html(_this.toCur(total));
    return total;
  },
  toCur: function (val) {
    return vnd.format(val);
  },
};
function price_format(){
    $('.price-format').each(function(){
        var $price = $(this).data('price'),
            html=vnd.format($price);
        $(this).html(html);
    });
}
$(function(){
    price_format();
});
// ===================Thuy detail=====================
/***AVATAR SCRIPT***/

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
// ======================pagination====================
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
    return range(1, maxLength - sideWidth - 1).concat(
      0,
      range(totalPages - sideWidth + 1, totalPages)
    );
  }

  if (page >= totalPages - sideWidth - 1 - rightWidth) {
    return range(1, sideWidth).concat(
      0,
      range(totalPages - sideWidth - 1 - rightWidth - leftWidth, totalPages)
    );
  }

  return range(1, sideWidth).concat(
    0,
    range(page - leftWidth, page + rightWidth),
    0,
    range(totalPages - sideWidth + 1, totalPages)
  );
}

$(function () {
  var numberOfItems = $(".content__list, .room-item").length;
  var limitPerPage = 9;
  var totalPages = Math.ceil(numberOfItems / limitPerPage);
  var paginationSize = 7;
  var currentPage;

  function showPage(whichPage) {
    if (whichPage < 1 || whichPage > totalPages) return false;
    currentPage = whichPage;
    $(".content__list .room-item")
      .hide()
      .slice((currentPage - 1) * limitPerPage, currentPage * limitPerPage)
      .show();
    $(".pagination li").slice(1, -1).remove();
    getPageList(totalPages, currentPage, paginationSize).forEach((item) => {
      $("<li>")
        .addClass("pages")
        .addClass("page-item")
        .addClass(item ? "current-page" : "dots")
        .toggleClass("active", item === currentPage)
        .append(
          $("<a>")
            .addClass("page-link")
            .attr({ href: "javascript: void(0)" })
            .text(item || "...")
        )
        .insertBefore(".next-page");
    });

    $(".previous-page").toggleClass("disabled", currentPage === 1);
    $(".next-page").toggleClass("disabled", currentPage === totalPages);
    return true;
  }
  $(".pagination").append(
    $("<li>")
      .addClass("page-item")
      .addClass("previous-page")
      .append(
        $("<a>")
          .addClass("page-link")
          .addClass("btn")
          .attr({ href: "javascript: void(0)" })
          .append($("<i>").addClass("fa fa-angle-left"))
      ),
    $("<li>")
      .addClass("page-item")
      .addClass("next-page")
      .append(
        $("<a>")
          .addClass("page-link")
          .addClass("btn")
          .attr({ href: "javascript: void(0)" })
          .append($("<i>").addClass("fa fa-angle-right"))
      )
  );

  $(".content__list").show();
  showPage(1);

  $(document).on(
    "click",
    ".pagination li.current-page:not(.active)",
    function () {
      return showPage(+$(this).text());
    }
  );

  $(".next-page").on("click", function () {
    return showPage(currentPage + 1);
  });
  $(".previous-page").on("click", function () {
    return showPage(currentPage - 1);
  });
});
