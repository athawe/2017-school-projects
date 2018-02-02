<?php
/**
 * Project 1 File Doc Comment
 *
 * @category Project_1
 * @package  N/A
 * @author   Aaron Thawe <aaron.thawe@gmail.com>
 * @license  http://www.gnu.org/copyleft/gpl.html GNU General Public License
 * @version  N/A
 * @link     aaron.thawe@gmail.com *
 */
session_start(); //Send user to other pages if they should be
if (!($_SERVER['REQUEST_METHOD'] == 'POST')) {
    //check for back button
    if (($_SESSION['pagetracker'] < 2) || !(isset($_SESSION['pagetracker']))) {
        echo "<script>location.assign('./page1.php');</script>";
    } elseif ($_SESSION['pagetracker'] == 3) {
        echo "<script>location.assign('./page3.php');</script>";
    } elseif ($_SESSION['pagetracker'] == 4) {
        echo "<script>location.assign('./thankpage.php');</script>";
    }
}
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    // check if user going back
    if (((isset($_POST['btnBack'])) && ($_POST['btnBack'] == "Previous"))) {
        $_SESSION['pagetracker'] = 1;
        echo "<script>location.assign('./page1.php');</script>";
    }
    //boolean to confirm later if any validation issues found or not
    $_SESSION['page2valid'] = true;

    //Validate purchase radio buttons
    if (!(isset($_POST['howPurchased']))) {
        $_SESSION['page2valid'] = false;
        $_SESSION['validArray2'][1] = 1;
    } else {
        $_SESSION['validArray2'][1] = 0;
    }

    //Validate purchases array
    if (!(isset($_POST['purchases']))) {
        $_SESSION['page2valid'] = false;
        $_SESSION['validArray2'][2] = 1;
    } else {
        $_SESSION['validArray2'][2] = 0;
    }

    //fill in session values for the page for repopulating values if needed, and also for storage if moving on
    if (isset($_POST['howPurchased'])) {
        $_SESSION['howPurchased'] = $_POST['howPurchased'];
    }
    if (isset($_POST['purchases'])) {
        $_SESSION['purchases'] = $_POST['purchases'];
    }
    //decide to move on or not
    if ($_SESSION['page2valid'] == true) {
        moveForwardPage2();
    } else {
        page2Form();
    }
} else {
    //only runs when page is being freshly opened
    $_SESSION['purchases'] = [];
    $_SESSION['validArray2'][1] = 0;
    $_SESSION['validArray2'][2] = 0;
    // output form to user
    page2Form();
}
// send user forward
function moveForwardPage2()
{
    $_SESSION['pagetracker'] = 3;
    echo "<script>location.assign('./page3.php');</script>";
}
//print form to suer
function page2Form()
{
    // set variable to repopulate data
    if (isset($_SESSION['howPurchased'])) {
        $howPurchased = $_SESSION['howPurchased'];
    } else {
        $howPurchased = "";
    }
    ?>
    <header>
        <h1>Sony Satisfaction Survey: Page 2</h1>
        <hr/>
    </header>
    <p>Here's the second set of questions:</p>
    <!-- print form to user -->
    <form method="POST" action="./page2.php">
        <label>How did you complete your purchase? </label>
        <input type="radio" id="option1"
        name="howPurchased" value="online" 
        <?php if ($howPurchased == "online") {
            echo "checked";
}?>>
        <label for="option1">Online</label>

        <input type="radio" id="option2"
        name="howPurchased" value="phone"
        <?php if ($howPurchased == "phone") {
            echo "checked";
}?>>
        <label for="option2">By phone</label>

        <input type="radio" id="option3"
        name="howPurchased" value="mobile" 
        <?php if ($howPurchased == "mobile") {
            echo "checked";
}?>>
        <label for="option3">Mobile App</label>

        <input type="radio" id="option4"
        name="howPurchased" value="store" 
        <?php if ($howPurchased == "store") {
            echo "checked";
}?>>
        <label for="option4">In store</label>
        <!-- print out validation if needed -->
        <p id="valHowPurchased">
        <?php if ($_SESSION['validArray2'][1] == 1 && isset($_SESSION['validArray2'][1])) {
            echo "One of these options must be checked.";
}?></p>
        <br>
        <label>Which of the following did you purchase? </label>
        <input type="checkbox" id="phoneChk" name="purchases[]" value="phone" 
        <?php if (in_array("phone", $_SESSION['purchases'])) {
            echo "checked";
}?>>
        <label for="phoneChk">Phone </label>

        <input type="checkbox" id="smartChk" name="purchases[]" value="smart" 
        <?php if (in_array("smart", $_SESSION['purchases'])) {
            echo "checked";
}?>>
        <label for="smartChk">Smart TV </label>

        <input type="checkbox" id="laptopChk" name="purchases[]" value="laptop" 
        <?php if (in_array("laptop", $_SESSION['purchases'])) {
            echo "checked";
}?>>
        <label for="laptopChk">Laptop </label>

        <input type="checkbox" id="tabletChk" name="purchases[]" value="tablet" 
        <?php if (in_array("tablet", $_SESSION['purchases'])) {
            echo "checked";
}?>>
        <label for="tabletChk">Tablet </label>

        <input type="checkbox" id="homeChk" name="purchases[]" value="home" 
        <?php if (in_array("home", $_SESSION['purchases'])) {
            echo "checked";
}?>>
        <label for="homeChk">Home Theater </label>
        <!-- print out validation output if required -->
        <p id="valPurchases">
        <?php if ($_SESSION['validArray2'][2] == 1) {
            echo "You must select at least one of these options.";
}?></p>
            <br>
        <!-- buttons to send user forward or back -->
        <input type="submit" name="nextbutton" value="Next!"/>
        <input type="button" name="btnBack" value="Previous"/>
    </form>
    <footer>
        <hr/>
        <p>Copyright Aaron Thawe</p>
    </footer>
<?php } ?>
<html>
<head>
    <title>Second Page</title>
</head>

<body>
</body>
</html>