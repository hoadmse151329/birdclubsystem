let toggle = document.querySelector('.toggle');
let navigation = document.querySelector('.navigation');
let header = document.querySelector('.header');
let main = document.querySelector('.main-content');

toggle.onclick = function () {
    navigation.classList.toggle('active');
    header.classList.toggle('active');
    main.classList.toggle('active');
};

$('.nav-link').each(function(i){
    if(i === 0){
        $(this).addClass('active');
    }
});
$('.tab-pane').each(function(i){
    if(i === 0){
        $(this).addClass('active');
    }
});


//loading
// $(window).on("load",function () {
//     $(".preloader").fadeOut("slow");
//     $(".preloader").css("display","none");
// });


let feedback = document.querySelectorAll('.feedback');
feedback.onclick = function () {
    $("#" + (this).data("data-target")).classList.toggle('active'); 
}


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
        reader.onload=()=> {
            let img = document.createElement("img");
            img.setAttribute("src",reader.result);
            figure.insertBefore(img,figCap);
        }
        imageContainer.appendChild(figure);
        reader.readAsDataURL(i);
    }
}


$(document).ready(function () {
    //data-table
    $('#myTable').DataTable();

    $("td:has(#input)").click(function() {
        $("#input", this).removeAttr("disabled");
    });
} );


$(document).ready(function () {
    
    $(".clickable-row").click(function() {
        window.location = $(this).data("href");
    });
})


$(document).ready(function () {
    setTimeout(function () {
        $('#notif').css('display', 'none');
    }, 3000);
})

//profile
var loadFile = function (event) {
    var image = document.getElementById("output");
    image.src = URL.createObjectURL(event.target.files[0]);
};  


function timeFrame(period) {
    if (period.value == 'day') {
        chart.data.datasets[0].data = day;
        chart.options.scales.x.time.unit = period.value;
    }
    if (period.value == 'week') {
        chart.data.datasets[0].data = week;
        chart.options.scales.x.time.unit = period.value;

    }
    if (period.value == 'month') {
        chart.data.datasets[0].data = month;
        chart.options.scales.x.time.unit = period.value;
    }
    chart.update();
} 


//change idHome for delete
function change(event) {
    document.getElementById("idhome").value = event.target.value;
}


// input other reason
function other(event) {
    if (event.target.value === 'custom') {
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

function myFunc() {
    var list = [];
    var sel1 = $('#motelid').val();
    
    const option = document.getElementById('typeRoom');

    for (let i = 0; i < option.childElementCount; i++) {
        list[i] = option.children[i].getAttribute('data-option');
    }

    list.forEach((item, index) => {
         if(item === sel1 || item == null) {
            option.children[index].style.display = "block";
         } else {
            option.children[index].style.display = "none";
         }
    });
}



// sweetalert
function submitFunc() {
    swal({
        title: "Successfully!",
        icon: "success",
        timer: 1000
    }).then(() => {
        document.getElementById('form').submit();
    });
    return false;
};

// confirm to dele

function confirmdele() {
    if (!confirm('Are you sure to delete?')) { 
        return false }
}




 function getValue() {

    const numOfHome = document.querySelectorAll('#numOfHome select');
    var num = [];
    var lengthOfarray;
    for (let i = 0; i < numOfHome.length; i++) {
        var data = document.getElementById(numOfHome[i].id);
        lengthOfarray = data.length;
        for (let j = 0; j < lengthOfarray; j++) {
            num.push({x : Date.parse(data.options[j].innerText), y : data.options[j].value})
        }  
    }
    console.log(num);
    

    // create array ojbect
    var home1 = [];
    let a1234 = document.getElementById('a1234');
    for (let i = 0; i < a1234.length; i++) {
        home1.push({x : Date.parse(a1234.options[i].innerText), y : a1234.options[i].value})
    }
    var home2 = [];
    let b1234 = document.getElementById('b1234');
    for (let i = 0; i < b1234.length; i++) {
        home2.push({x : Date.parse(b1234.options[i].innerText), y : b1234.options[i].value})
    }
    const ctx = document.getElementById('chart-Dashboard').getContext('2d');
    const chart = new Chart(ctx, {   
            type: 'bar',
            data: {
                // labels: Object.keys(week),
                datasets: [{
                    label: 'Doanh thu nhà nghỉ A',
                    data: home1,
                    backgroundColor: [
                        'rgba(39, 174, 96,0.5)'
                    ],
                    borderColor: [
                        'rgba(39, 174, 96,1.0)'
        
                    ],
                    borderWidth: 1,
                },{
                    label: 'Doanh thu nhà nghỉ B',
                    data: home2,
                    backgroundColor: [
                        'rgba(192, 174, 96,0.5)'
                    ],
                    borderColor: [
                        'rgba(39, 174, 96,1.0)'
        
                    ],
                    borderWidth: 1,
                }]
            },
            options: {
                scales: {
                    x: {
                        type: 'time',
                        time: {
                            unit: 'day'
                        }
                    },
                    y: {
                        beginAtZero: true
                    }
                }
            }
        }); 
}   



