

document.getElementById("tab-signup").addEventListener("click", LogIn);
document.getElementById("tab-login").addEventListener("click", SignUp);



document.getElementById("login").style.display = "none";

function SignUp() {

    document.getElementById("signup").style.display = "none";
    document.getElementById("login").style.display = "block";

    var login = document.getElementById("tab-login");
    var signUp = document.getElementById("tab-signup");

    login.style.background = "#6699ff";
    signUp.style.background = "#99bbff";

    
    
}
function LogIn() {
    document.getElementById("signup").style.display = "block";
    document.getElementById("login").style.display = "none";

    var login = document.getElementById("tab-login");
    var signUp = document.getElementById("tab-signup");

    login.style.background = "#99bbff";
    signUp.style.background = "#6699ff";

    
}
function checkEmail() {
    var e = $("#EmailAddress").val();
    var button = document.getElementById("Submit1");
    $.ajax({
        type: 'GET',
        url: '/Home/EmailExistingCheck',
        datatype: 'json',
        data: { emailAddress: e },
        contentType: 'aplication/json; charset=utf-8',
        success: function (result) {
            var mass = $("#ErrorMass");
            if (!result) {
                mass.html("This Email already taken.").css('color', 'red');
                button.disabled = true;
            }
            else {
                mass.html("");
                button.disabled = false;
            }
                

        }
    });
};


