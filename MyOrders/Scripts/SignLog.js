

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
