
    //document.addEventListener("DOMContentLoaded", () => {
    //  const track = document.getElementById("eventCarouselTrack");
    //const visibleCount = 3;
    //const cardWidth = 400 + 20; // width + padding
    //let currentIndex = 0;
    //let intervalId;

    //const cards = [...track.children];
    //const totalSlides = cards.length;

    //// Clone first visibleCount cards
    //for (let i = 0; i < visibleCount; i++) {
    //    const clone = cards[i].cloneNode(true);
    //track.appendChild(clone);
    //  }

    //function startCarousel() {
    //    intervalId = setInterval(() => {
    //        currentIndex++;
    //        track.style.transition = "transform 0.5s ease-in-out";
    //        track.style.transform = `translateX(-${currentIndex * cardWidth}px)`;

    //        if (currentIndex === totalSlides) {
    //            setTimeout(() => {
    //                track.style.transition = "none";
    //                currentIndex = 0;
    //                track.style.transform = "translateX(0)";
    //            }, 500);
    //        }
    //    }, 3000);
    //  }

    //function stopCarousel() {
    //    clearInterval(intervalId);
    //  }

    //// Start initially
    //startCarousel();

    //// Pause on hover
    //track.addEventListener("mouseenter", stopCarousel);
    //track.addEventListener("mouseleave", startCarousel);
    //});

