$(document).ready(function () {
    // Show loading screen when navigating to a new page
    $('a').click(function () {
        $('#loading-screen').fadeIn();
    });
    // Show loading screen on page load
    $('#loading-screen').fadeIn();

    // Hide loading screen after a delay (e.g., 3 seconds)
    setTimeout(function () {
        $('#loading-screen').fadeOut();
    }, 2000); // Adjust the delay time as needed (in milliseconds)
});

const textAnimation = document.querySelector('.text-animation');

// Set animation duration in milliseconds
const animationDuration = 5000; // 5 seconds

// Set the interval for updating the text color gradient
const interval = 30; // 50 milliseconds

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
