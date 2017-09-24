
$(document).ready(function () {

    var userRows = document.getElementsByClassName("UserRow");
    for (var i = 0; i < userRows.length; i++) {
        $(userRows[i]).css("top", "" + i * 5 + "%");
    }

    $('.UserRow').mouseenter(function() {
       $(this).children('#namediv').css("background-color", "#4dd2ff");
       $(this).children('#orgdiv').css("background-color", "#4dd2ff");
       $(this).children('#loginnamediv').css("background-color", "#4dd2ff");
       $(this).children('#countrydiv').css("background-color", "#4dd2ff");
    });
    $('.UserRow').mouseout(function() {
        $(this).children('#namediv').css("background-color", "#80dfff");
        $(this).children('#orgdiv').css("background-color", "#80dfff");
        $(this).children('#loginnamediv').css("background-color", "#80dfff");
        $(this).children('#countrydiv').css("background-color", "#80dfff");
    });

    $('.ManageButton').click(function() {
        try {
            var builderId = $(this).attr('id');
            document.getElementById('hidden').value = builderId;
            $('.dummyBuilderIdBtnManage')[0].click();
        } catch (e) {
            alert(e);
        }
    });

    $('.DeleteButton').click(function () {
        try {
            var builderId = $(this).attr('id');
            document.getElementById('hidden').value = builderId;
            $('.dummyBuilderIdBtnDelete')[0].click();
        } catch (e) {
            alert(e);
        }
    });


    var projectRows = document.getElementsByClassName("ProjectRow");
    for (var j = 0; j < projectRows.length; j++) {
        $(projectRows[j]).css("top", "" + j * 5 + "%");
    }
    $('.ProjectRow').mouseenter(function () {
        $(this).children('#projectnamediv').css("background-color", "#4dd2ff");
        $(this).children('#projectbuilderdiv').css("background-color", "#4dd2ff");
        $(this).children('#projectorgdiv').css("background-color", "#4dd2ff");
    });
    $('.ProjectRow').mouseout(function () {
        $(this).children('#projectnamediv').css("background-color", "#80dfff");
        $(this).children('#projectbuilderdiv').css("background-color", "#80dfff");
        $(this).children('#projectorgdiv').css("background-color", "#80dfff");
    });

    $('.ManageButtonProject').click(function () {
        try {
            var projectId = $(this).attr('id');
            document.getElementById('hiddenProject').value = projectId;
            $('.dummyProjectIdBtnManage')[0].click();
        } catch (e) {
            alert(e);
        }
    });

    $('.DeleteButtonProject').click(function () {
        try {
            var projectId = $(this).attr('id');
            document.getElementById('hiddenProject').value = projectId;
            $('.dummyProjectIdBtnDelete')[0].click();
        } catch (e) {
            alert(e);
        }
    });

});

function deleteSuccess(element) {
    alert(element + " deleted successfully.");
    if (element === "Project")
        $('.projectBtn')[0].click();
    else
        $('.builderBtn')[0].click();
}

function errorFunc(errorMsg) {
    alert("Error : " + errorMsg);
}

function userNameExistsError() {
    alert("This login name already exists. Please enter different login name.");
}

function userUpdated() {
    alert("User information successfully updated.");
    $('.builderBtn')[0].click();
}

function projectAdded() {
    alert("Project added successfully.");
    $('.projectBtn')[0].click();
}

function projectUpdated() {
    alert("Project information successfully updated.");
    $('.projectBtn')[0].click();
}

function projectNameExists() {
    alert("This project name already exists for the selected builder. Please enter a different project name.");
}