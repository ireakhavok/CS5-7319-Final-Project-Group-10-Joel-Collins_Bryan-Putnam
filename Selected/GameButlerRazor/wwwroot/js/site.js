// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", function () {
    const showPassword = document.querySelector("#show-password");
    const passwordField = document.querySelector("#login-form");

    showPassword.addEventListener("click", function () {
        this.classList.toggle("fa-eye");
        this.classList.toggle("fa-eye-slash"); 

        const type = passwordField.getAttribute("type")
            === "password" ? "text" : "password"; 
        passwordField.setAttribute("type", type); 
    })
})
function Pkcs1Pad2(data, keysize) {
    var _keysize = parseInt(keysize) ;
    //if (keySize < data.Length + 11) {
    //    return new BigInteger();
    //}
    var buffer = new ArrayBuffer(keysize);
    var i = data.length - 1;

    while ((i >= 0) && (_keysize > 0)) {
        buffer[--_keysize] = charToByte(data[i--]);
    }

    // Padding, I think

    buffer[--_keysize] = 0;
    while (_keysize > 2) {
        min = Math.ceil(1);
        max = Math.floor(255);
        buffer[--_keysize] = Math.floor(Math.random() * (max - min + 1)) + min;
    }

    buffer[--_keysize] = 2;
    buffer[--_keysize] = 0;

    //buffer = buffer.Reverse;
    //Array.Reverse(buffer);
    var reversed = reverseBuffer(buffer);

    return littleEndianBigInteger(reversed);
}
function littleEndianBigInteger(buffer) {
    //taken from https://stackoverflow.com/questions/73181182/converting-uint8array-to-bigint-in-javascript

    let result = BigInt(0);
    for (let i = buffer.byteLength; i > 0; i--) {
        result = result * BigInt(256) + BigInt(buffer[i]);
    }
    return result;
}
function reverseBuffer(buffer) {
    var size = buffer.byteLength
    var reversed = new ArrayBuffer(size);
    for (let i = size - 1; i > -1; i -= 1) {
        reversed[-i + size] = buffer[i];
    }
    return reversed;
}
function hexToBytes(hex) {
    if (hex.length % 2) { hex = '0' + hex; }
    var bn = BigInt('0x' + hex);
    return bn;
}
function charToByte(str) {
    return str.charCodeAt(0);
}
function getBigIntLength(tempBigNumber) {
    //const i = (x.toString(16).length - 1) * 4
    //return i + 32 - Math.clz32(Number(x >> BigInt(i)))
    let i = 0;
    while (tempBigNumber > BigInt(0)) {
        tempBigNumber = tempBigNumber / BigInt(256);
        i += 1;
    }
    return i;
}
function toLittleEndianByteArray(bigNumber) {
    var length = getBigIntLength(bigNumber)
    let result = new Uint8Array(length);
    let i = 0;
    while (bigNumber > BigInt(0)) {
        result[i] = Number(bigNumber % BigInt(256));
        bigNumber = bigNumber / BigInt(256);
        i += 1;
    }
    return result;
}
function arrayBufferToBase64(buffer) {
    //taken from https://stackoverflow.com/questions/55356285/how-to-convert-a-string-to-base64-encoding-using-byte-array-in-javascript
    var binary = '';
    //var bytes = new Uint8Array(buffer);
    var len = buffer.byteLength;
    for (var i = 1; i < len +1; i++) {
        binary += String.fromCharCode(buffer[i]);
    }
    return btoa(binary);
}
function EncryptPasswordRound() {
    var username = document.getElementById("username").value;
    var password = document.getElementById("login-form").value;
    if (username.trim() === "" || password.trim() === "") {
        //.value returns string
        alert("Username or Password is Empty");
    }
    else {
        var publickey_mod = document.getElementById("publickey_mod").value;
        var publickey_exp = document.getElementById("publickey_exp").value;
        var timestamp = document.getElementById("timestamp").value;
        var success = document.getElementById("success").value;

        var h_publickey_mod = hexToBytes(publickey_mod);
        var h_publickey_exp = hexToBytes(publickey_exp);
        //var encryptedNumber = Pkcs1Pad2(rsaParam.Password, (2048 + 7) >> 3);
        var encryptedNumber = Pkcs1Pad2(password, (2048 + 7) >> 3);

        // And now, the RSA encryption
        //encryptedNumber = expmod(encryptedNumber, h_publickey_exp, h_publickey_mod);
        encryptedNumber = calculateRemainder(encryptedNumber, h_publickey_exp, h_publickey_mod);

        var encryptedNumberByteArray = toLittleEndianByteArray(encryptedNumber);

        var reversed = reverseBuffer(encryptedNumberByteArray);
        //Reverse number and convert to base64
        var encryptedString = arrayBufferToBase64(reversed);

        return encryptedString;
    }
}
$('#login').one('submit', function () {

    encryptPassword();

    $("#login").submit();
});
$('.tableRow').on('click', function () {

    var win = window.open($(this).val(), '_blank');
    if (win) {
        //Browser has allowed it to be opened
        //if (document.getElementById("switchFocus").checked) {
        //    win.focus();
        //}
    } else {
        //Browser has blocked it
        alert('Please allow popups for this website');
    }
});
function isDate(str)
{
    var valid = (new Date(str)).getTime() > 0;
    return valid;
}
function isNumeric(str) {
    //taken from https://stackoverflow.com/questions/175739/how-can-i-check-if-a-string-is-a-valid-number
    if (typeof str != "string") return false // we only process strings!  
    return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
        !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
}

function sortTable(n) {
    //taken from https://www.w3schools.com/howto/howto_js_sort_table.asp
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("GameTable");
    switching = true;
    // Set the sorting direction to ascending:
    dir = "asc";
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /* Loop through all table rows (except the
        first, which contains table headers): */
        for (i = 1; i < (rows.length - 1); i++) {
            // Start by saying there should be no switching:
            shouldSwitch = false;
            /* Get the two elements you want to compare,
            one from current row and one from the next: */
            x = rows[i].getElementsByTagName("TD")[n].textContent.trim().toLowerCase()
            y = rows[i + 1].getElementsByTagName("TD")[n].textContent.trim().toLowerCase()
            /* Check if the two rows should switch place,
            based on the direction, asc or desc: */
            if (dir == "asc") {
                if (isNumeric(x) && isNumeric(y)) {
                    if (Number(x) > Number(y)) {
                        // If so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                }
                else if (isDate(x) && isDate(y))
                {
                    if ((new Date(x)).getTime() > (new Date(y)).getTime()) {
                        shouldSwitch = true;
                        break;
                    }
                }
                else if (x > y) {
                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (isNumeric(x) && isNumeric(y)) {
                    if (Number(x) < Number(y)) {
                        // If so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                }
                else if (isDate(x) && isDate(y)) {
                    if ((new Date(x)).getTime() < (new Date(y)).getTime()) {
                        shouldSwitch = true;
                        break;
                    }
                }
                else if (x < y) {
                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark that a switch has been done: */
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            // Each time a switch is done, increase this count by 1:
            switchcount++;
        } else {
            /* If no switching has been done AND the direction is "asc",
            set the direction to "desc" and run the while loop again. */
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}
function encryptPassword() {

    var encrypted = "";
    while (encrypted.length < 2) {
        encrypted = EncryptPasswordRound();
    }
    document.getElementById("login-form").value = encrypted;
}
//function expmod(base, exp, mod) {
//    if (exp == BigInt(0)) {
//        return BigInt(1);
//    } 
//    if (exp % BigInt(2) == BigInt(0)) {
//        return (expmod(base, (exp / BigInt(2)), mod) ** BigInt(2)) % mod;
//    }
//    else {
//        return (base * expmod(base, (exp - BigInt(1)), mod)) % mod;
//    }
//}
function calculateRemainder(value, exponent, modulus) {
    var result = BigInt(value) ** BigInt(exponent) % BigInt(modulus);
    return result;
}
function loginCredentials() {
    // Retrieve Username and Password values from Login.cshtml
    var username = document.getElementById("user").value;
    var password = document.getElementById("login-form").value;

    if (username.trim() === "" || password.trim() === "") {
        //.value returns string
        alert("Username or Password is Empty"); 
    }
    else { 

        //const body = {
        //    username: username
        //};
        //$.post("https://steamcommunity.com/login/getrsakey", body, (data, status) => {
        //    console.log(data);
        //});

        //const xhr = new XMLHttpRequest();
        //xhr.open("POST", "https://steamcommunity.com/login/getrsakey");
        //xhr.setRequestHeader("Content-Type", "application/json; charset=UTF-8");
        //const body = JSON.stringify({
        //    username: username
        //});
        //xhr.onload = () => {
        //    if (xhr.readyState == 4 && xhr.status == 201) {
        //        console.log(JSON.parse(xhr.responseText));
        //    } else {
        //        console.log(`Error: ${xhr.status}`);
        //    }
        //};
        //xhr.send(body);

        //await fetch("https://steamcommunity.com/login/getrsakey", {
        //    method: "POST",
        //    body: JSON.stringify({
        //        username: username
        //    }),
        //    headers: {
        //        "Content-type": "application/json; charset=UTF-8"
        //    }
        //}).then((response) => response.json())
        //  .then((json) => console.log(json));
            //

        // encode Username and Password
        var encodedPassword = btoa(password);

        console.log("Username: ", username);
        console.log("Password: ", password);
        console.log("Encoded Password: ", encodedPassword);
    }
    

}
