document.getElementById("ImageFile").addEventListener("change", function () {
    var allowed = [".jpg", ".jpeg", ".png", ".webp"];
    var fileName = this.value.toLowerCase();
    var isValid = allowed.some(ext => fileName.endsWith(ext));

    var validationSpan = document.querySelector('[data-valmsg-for="ImageFile"]');

    if (!isValid) {
        validationSpan.textContent = "Invalid file type. Only JPG, JPEG, PNG, and WEBP files are allowed.";
        validationSpan.classList.add("text-danger");
        this.value = "";
    } else {
        validationSpan.textContent = "";
    }
});
