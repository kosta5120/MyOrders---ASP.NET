

document.getElementById("tab-signup").addEventListener("click", LogIn);
document.getElementById("tab-login").addEventListener("click", SignUp);


document.getElementById("login").style.display = "none";

function SignUp() {

    document.getElementById("signup").style.display = "none";
    document.getElementById("login").style.display = "block";
    
    
}
function LogIn() {
    document.getElementById("signup").style.display = "block";
    document.getElementById("login").style.display = "none";
    var x = document.getElementById("tab-signup");
    x.satyle.backgroundColor = "blue";
    
}
