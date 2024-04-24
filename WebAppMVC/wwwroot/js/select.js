
// select following select box
var sel1 = document.querySelector('#motelId');
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
