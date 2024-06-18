var speed = 1; // change scroll speed with this value
/**
* Initialize the marquee, and start the marquee by calling the marquee function.
*/
function init() {
    var el = document.getElementById("marquee_replacement");
    el.style.overflow = 'hidden';
    scrollFromBottom();
}

var go = 0;
var timeout = '';
/**
* This is where the scroll action happens.
* Recursive method until stopped.
*/
function scrollFromBottom() {
    clearTimeout(timeout);
    var el = document.getElementById("marquee_replacement");
    if (el.scrollTop >= el.scrollHeight - 200) {
        el.scrollTop = 0;
    };
    el.scrollTop = el.scrollTop + speed;
    if (go == 0) {
        timeout = setTimeout("scrollFromBottom()", 40);
    };
}

/**
* Set the stop variable to be true (will stop the marquee at the next pass).
*/
function stop() {
    go = 1;
}

/**
* Set the stop variable to be false and call the marquee function.
*/
function startit() {
    go = 0;
    scrollFromBottom();
}