document.addEventListener('DOMContentLoaded', function () {
    const navbar = document.querySelector('nav.navbar');

    function toggleNavbarBg() {
            if (window.scrollY >=300) {
        navbar.classList.add('navbar-scroll');
            } else {
        navbar.classList.remove('navbar-scroll');
            }
    }

    window.addEventListener('scroll', toggleNavbarBg);

    // Initialize on page load
    toggleNavbarBg();
});
