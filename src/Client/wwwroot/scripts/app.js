//toggle navigation menu for narrow screens
const marquees = document.getElementsByTagName("marquee").stop();


const openMenu = () => {
    const menu = document.querySelector(".navigation-options");
    console.log(menu);
    menu.style.display = menu.style.display == "flex" ? "none" : "flex";
};
