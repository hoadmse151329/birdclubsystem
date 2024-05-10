$(document).ready(function () {
    // Initialize the loading screen to be hidden
    $('#loading-screen').hide();

    // Show loading screen when a link with the specific class is clicked
    $('.specific-button-class').click(function (e) {
        e.preventDefault(); // Prevent default behavior of the link
        $('#loading-screen').fadeIn(); // Show loading screen
        setTimeout(function () {
            window.location.href = $(this).attr('href'); // Navigate to the clicked link after a delay
        }.bind(this), 1000); // Adjust the delay time as needed (in milliseconds)
    });

    // Hide loading screen when the page finishes loading
    $(window).on('load', function () {
        $('#loading-screen').fadeOut();
    });
});

const textAnimation = document.querySelector('.text-animation');

// Set animation duration in milliseconds
const animationDuration = 2000; 

// Set the interval for updating the text color gradient
const interval = 30;

// Function to update text color gradient during animation
function updateTextColor() {
    const timeElapsed = Date.now() % animationDuration;
    const position = (timeElapsed / animationDuration) * 100;
    const color = getColorFromGradient(position);
    textAnimation.style.color = color;
}

// Function to calculate color from gradient
function getColorFromGradient(position) {
    const r = Math.round((255 * position) / 100);
    const g = Math.round((255 * (100 - Math.abs(50 - position) * 2)) / 100);
    const b = Math.round((255 * (100 - position)) / 100);
    return `rgb(${r},${g},${b})`;
}

// Set interval to update text color gradient during animation
setInterval(updateTextColor, interval);
