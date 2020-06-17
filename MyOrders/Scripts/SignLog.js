
document.getElementById("tab-signup").addEventListener("click", LogIn);
document.getElementById("tab-login").addEventListener("click", SignUp);
document.getElementById("Submit2").addEventListener("click", checkLogin);


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
                button.style.background = "Red";
                document.getElementById("Password").reset();
            }
            else {
                mass.html("");
                button.disabled = false;
                button.style.background = "#1ab188"
            }


        }
    });
}

function checkLogin() {

    var email = $("#EmailLogIn").val();
    var pass = $("#passLogIn").val();

    //e.preventDefault();

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/Home/CheckLogIn',
        //url: '/Home/LoginSignup',
        data: { email: email, pass: pass },
        success: function (data) {

            var mess = $("#ErrorMassLogIn");
            if (!data.result)
                mess.html("Email or Password is wrong.").css('color', 'red');
            else {
                mess.html("");
                var url = '/Orders/Orders/' + data.id;
                
                window.location.href = url;
            }
                
        }
    });
}


