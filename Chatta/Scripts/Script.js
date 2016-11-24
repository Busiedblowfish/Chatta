//Fix max text length to in textbox1 to 160 characters, ignore copy and paste situations
function howMany(text)
{
    var maxlength = 160; //set your value here (or add a parm and pass it in)
    var object = document.getElementById("TextBox1");  //get Textbox1 object
    if (object.value.length > maxlength)
    {
        object.focus(); //set focus to prevent jumping
        object.value = text.value.substring(0, maxlength); //truncate the value
        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        return false;
    }
    return true;

}