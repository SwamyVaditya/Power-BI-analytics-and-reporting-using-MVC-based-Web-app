function daoInitError(errorMsg) {
    alert("Error in creating new user !!!. Please try later.\n Error : "+ errorMsg);
}

function userNameExistsError() {
    alert("This login name already exists. Please enter different login name.");
}

function userRegistered() {
    alert("User successfully registered.");
    window.location.href = "AdminPage.aspx";
}

function clearForm() {
    document.getElementById("name").value = "";
    document.getElementById("mail").value = "";
    document.getElementById("loginname").value = "";
    document.getElementById("project_id").value = "";
    document.getElementById("phone").value = "";
    document.getElementById("city").value = "";
    document.getElementById("state").value = "";
    document.getElementById("country").value = "";
}
