//toggle navigation menu for narrow screens
const openMenu = () => {
    const menu = document.querySelector(".navigation-options");
    console.log(menu);
    menu.style.display = menu.style.display == "flex" ? "none" : "flex";
};
