
let vnd = Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
    useGrouping: true
}); 
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
let toggle = document.querySelector('.toggle');
let navigation = document.querySelector('.navigation');
let header = document.querySelector('.header');
let main = document.querySelector('.main-content');

toggle.onclick = function () {
    navigation.classList.toggle('active');
    header.classList.toggle('active');
    main.classList.toggle('active');
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

//data-table

$(document).ready( function () {
    $('#myTable').DataTable();
} );

//profile
var loadFile = function (event) {
    var image = document.getElementById("output");
    image.src = URL.createObjectURL(event.target.files[0]);
  };  

let label = document.querySelector(".label");
let hover = document.querySelector(".profile-pic");

profileAvat = function () {
    label.classList.toggle(".active");
}


//chart
const month = Utils.month({count: 7});
const ctx = document.getElementById('myChart');
const myChart = new Chart(ctx, {   
    type: 'bar',
    data: {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [{
            label: '# of Votes',
            data: [12, 19, 3, 5, 2, 3],
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)',
                'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1
        }]
    },
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

$(document).ready( function () {
    $('#myTable').DataTable();
} );
