let toggle = document.querySelector(".toggle");
let navigation = document.querySelector(".navigation");
let header = document.querySelector(".header");
let main = document.querySelector(".main-content");


toggle.onclick = function () {
  navigation.classList.toggle("active");
  header.classList.toggle("active");
  main.classList.toggle("active");
};

$(".nav-link").each(function (i) {
  if (i === 0) {
    $(this).addClass("active");
  }
});
$(".tab-pane").each(function (i) {
  if (i === 0) {
    $(this).addClass("active");
  }
});

//print bill
$('.btn-print').click(function () {
    var table = document.getElementById("table");
    var wme = window.open("","","with=900,height=700");
    wme.document.write(table.outerHTML);
    wme.document.close();    
    wme.focus();
    wme.print();
    wme.close();

})

//loading
// $(window).on("load",function () {
//     $(".preloader").fadeOut("slow");
//     $(".preloader").css("display","none");
// });

let vnd = Intl.NumberFormat("vi-VN", {
  style: "currency",
  currency: "VND",
  useGrouping: true,
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

let feedback = document.querySelectorAll(".feedback");
feedback.onclick = function () {
  $("#" + this.data("data-target")).classList.toggle("active");
};

// input image
let fileInput = document.getElementById("file-input");
let imageContainer = document.getElementById("images");
let numOfFiles = document.getElementById("num-of-files");
let removeFiles = document.getElementById("removeFiles");

function preview() {
  imageContainer.innerHTML = "";
  numOfFiles.textContent = `${fileInput.files.length}
    Files Selected`;

  for (i of fileInput.files) {
    let reader = new FileReader();
    let figure = document.createElement("figure");
    let figCap = document.createElement("figcaption");

    figCap.innerText = i.name;
    figure.appendChild(figCap);
    reader.onload = () => {
      let img = document.createElement("img");
      img.setAttribute("src", reader.result);
      figure.insertBefore(img, figCap);
    };
    imageContainer.appendChild(figure);
    reader.readAsDataURL(i);
  }
}

$(document).ready(function () {
  //data-table
  $("#myTable").DataTable();

});


$(document).ready(function () {
  $(".clickable-row").click(function () {
    window.location = $(this).data("href");
  });
});

$(document).ready(function () {
  setTimeout(function () {
    $("#notif").css("display", "none");
  }, 3000);
});


//profile
var loadFile = function (event) {
  var image = document.getElementById("output");
  image.src = URL.createObjectURL(event.target.files[0]);
};

//change idHome for delete
function change(event) {
  document.getElementById("idhome").value = event.target.value;
}

// input other reason
function other(event) {
  if (event.target.value === "custom") {
    document.getElementById("otherid").style.display = "block";
    document.getElementById("otherid1").style.display = "block";
    document.getElementById("otherid2").style.display = "block";
    document.getElementById("customFile").style.display = "block";
    document.getElementById("labeldecp").style.display = "block";
    document.getElementById("decp").style.display = "block";
  } else {
    document.getElementById("otherid").style.display = "none";
    document.getElementById("otherid1").style.display = "none";
    document.getElementById("otherid2").style.display = "none";
    document.getElementById("customFile").style.display = "none";
    document.getElementById("labeldecp").style.display = "none";
    document.getElementById("decp").style.display = "none";
  }
}

// select following select box
var sel1 = document.querySelector('#clubId');
var sel2 = document.querySelector('#typeRoom');
var options2 = sel2.querySelectorAll('option');

function myFunc(selValue) {
  sel2.innerHTML = '';
  for(var i = 0; i < options2.length; i++) {
    if(options2[i].dataset.option === selValue) {
      sel2.appendChild(options2[i]);
    }
  }
  sel2.appendChild(options2[options2.length-1]);
}
giveSelection(sel1.value);

// sweetalert
function submitFunc() {
  swal({
    title: "Successfully!",
    icon: "success",
    timer: 1000,
  }).then(() => {
    document.getElementById("form").submit();
  });
  return false;
}


// confirm to dele
function confirmdele() {
  if (!confirm("Are you sure to delete?")) {
    return false;
  }
}


$('#checkAll').click(function () {    
  $('input:checkbox').prop('checked', this.checked);  
  if ($('#checkAll').prop('checked')) {
    document.getElementById("all-tip").style.display = "block";
  } else {
    document.getElementById("all-tip").style.display = "none";

  }
});





///statical
//const numOfHome = document.querySelectorAll('#numOfHome select');
//var num = [];
//
//var nameOfHome = [];
//for (let i = 0; i < numOfHome.length; i++) {
//    eval ('var array' + i + '= []');
//    var data = document.getElementById(numOfHome[i].id);  
//    nameOfHome.push(data.name);
//
//    for (let j = 0; j < data.length; j++) {
//      eval('array' + i).push({x : Date.parse(data.options[j].innerText), y : data.options[j].value})
//    }  
//    num.push(eval('array'+ i));
//}
//
//  const myCtx = document.getElementById('chart-Dashboard').getContext('2d');
//  const myChart = new Chart(myCtx, {   
//      type: 'line',
//      data: {
//          datasets: []
//      },
//      options: {
//          scales: {
//              x: {
//                  type: 'time',
//                  time: {
//                      unit: 'day'
//                  }
//              },
//              y: {
//                  beginAtZero: true
//              }
//          }
//      }
//  }); 
//
//    for (let i = 0; i < numOfHome.length; i++) {
//      
//      let maxVal = 0xFFFFFF; 
//      let randomNumber = Math.random() * maxVal; 
//      randomNumber = Math.floor(randomNumber);
//      randomNumber = randomNumber.toString(16);
//      let randColor = randomNumber.padStart(6, 0);   
//
//      const newData = {
//        label: `${nameOfHome[i]}`,
//        data: num[i],
//        backgroundColor: [
//            `#${randColor}`
//        ],
//        borderColor: [
//            `#${randColor}`
//    
//        ],
//        borderWidth: 1,
//      }
//    
//      myChart.data.datasets.push(newData);
//      myChart.update();
//    }
// 
//  //change-chart
//  function timeFrame(period) {
//    if (period.value === 'day') {
//      myChart.data.datasets[0].data = day;
//      myChart.options.scales.x.time.unit = period.value;
//
//    }
//    if (period.value === 'week') {
//      myChart.data.datasets[0].data = week;
//      myChart.options.scales.x.time.unit = period.value;
//
//    }
//    if (period.value === 'month') {
//      myChart.data.datasets[0].data = month;
//      myChart.options.scales.x.time.unit = period.value;
//    }
//    myChart.update();
//
//  } 

  





