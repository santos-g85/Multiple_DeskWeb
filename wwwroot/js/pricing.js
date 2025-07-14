document.addEventListener('DOMContentLoaded', function () {
    const customBorder = document.querySelector('.custom-border');

    window.addEventListener('resize', function () {
        if (window.innerWidth <= 768) {
            customBorder.classList.remove('border-4');
            customBorder.classList.add('border-0');
        }
    }
});

