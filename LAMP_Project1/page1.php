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
    if (($_SESSION['pagetracker'] == 0) || !(isset($_SESSION['pagetracker']))) {
        echo "<script>location.assign('./index.php');</script>";
    } elseif ($_SESSION['pagetracker'] == 2) {
        echo "<script>location.assign('./page2.php');</script>";
    } elseif ($_SESSION['pagetracker'] == 3) {
        echo "<script>location.assign('./page3.php');</script>";
    } elseif ($_SESSION['pagetracker'] == 4) {
        echo "<script>location.assign('./thankpage.php');</script>";
    }
}
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    //check for back button
    if (((isset($_POST['btnBack'])) && ($_POST['btnBack'] == "Previous"))) {
        $_SESSION['pagetracker'] = 0;
        echo "<script>location.assign('./index.php');</script>";
    }
    //boolean to confirm later if any validation issues found or not
    $_SESSION['page1valid'] = true;

    //Validate name
    if ($_POST['fullName'] == "") {
        $_SESSION['page1valid'] = false;
        $_SESSION['validArray'][1] = 1;
    } else {
        $_SESSION['validArray'][1] = 0;
    }
    //Validate age
    if ($_POST['age'] == "") {
        $_SESSION['page1valid'] = false;
        $_SESSION['validArray'][2] = 1;
    } elseif (!(is_numeric($_POST['age']))) {
        $_SESSION['page1valid'] = false;
        $_SESSION['validArray'][2] = 2;
    } else {
        $_SESSION['validArray'][2] = 0;
    }
    //Validate Student Status
    if ($_POST['student'] == "Default") {
        $_SESSION['page1valid'] = false;
        $_SESSION['validArray'][3] == 1;
    } else {
        $_SESSION['validArray'][3] == 0;
    }
    //fill in session values for the page for repopulating values if needed, and also for storage if moving on
    $_SESSION['fullName'] = $_POST['fullName'];
    $_SESSION['age'] = $_POST['age'];
    $_SESSION['student'] = $_POST['student'];
    //decide to move on or not
    if ($_SESSION['page1valid'] == true) {
        moveForward();
    } else {
        //reprint form if validation failed
        page1Form();
    }
} else {
    //populate empty session values
    $_SESSION['validArray'][1] = 0;
    $_SESSION['validArray'][2] = 0;
    $_SESSION['validArray'][3] = 0;
    page1Form();
}
//send user to next page
function moveForward()
{
    $_SESSION['pagetracker'] = 2;
    echo "<script>location.assign('./page2.php');</script>";
}
//printout form to user
function page1Form()
{
    //fill variables for repopulation
    if (isset($_SESSION['fullName'])) {
        $fullName = $_SESSION['fullName'];
    } else {
        $fullName = "";
    }
    if (isset($_SESSION['age'])) {
        $age = $_SESSION['age'];
    } else {
        $age = "";
    }
    if (isset($_SESSION['student'])) {
        $student = $_SESSION['student'];
    } else {
        $student = "";
    }?>
    <header>
        <h1>Sony Satisfaction Survey: Page 1</h1>
        <hr/>
    </header>
    <!--form output -->
    <p>Here's the first set of questions:</p>
    <form method="POST" action="./page1.php">
        <!--name section -->
        <label for="fullName">Full Name </label>
        <input type="text" size="20" maxlength="30" id ="fullName" name="fullName" value="<?php print $fullName;?>"/> 
        <p id="valFullname"><?php if ($_SESSION['validArray'][1] == 1 && isset($_SESSION['validArray'][1])) {
            echo "This field must not be empty.";
}?></p>
            <br>
        <!-- age section -->
        <label for="age">Your Age </label>
        <input type="text" size="20" maxlength="5" id ="age" name="age" value="<?php print $age;?>"/> 
        <p id="valAge"><?php if ($_SESSION['validArray'][2] == 1 && isset($_SESSION['validArray'][2])) {
            echo "This field must not be empty.";
} elseif ($_SESSION['validArray'][2] == 2 && isset($_SESSION['validArray'][2])) {
            echo "Your age must be numeric";
}?>
            <br>
        <!-- student status -->
        <label for="student">Are you a student? </label>
        <select id ="student" name="student" size="1">
        <!-- different potential setups based on previously selected values -->
        <?php if ($student == "Default") { ?>
            <option selected="selected" value="Default">Choose Status</option>
            <option value="Full">Full Time</option>
            <option value="Part">Part Time</option>
            <option value="Not">No</option>
        <?php } elseif ($student == "Full") { ?>
            <option value="Default">Choose Status</option>
            <option selected="selected" value="Full">Full Time</option>
            <option value="Part">Part Time</option>
            <option value="Not">No</option>
        <?php } elseif ($student == "Part") { ?>
            <option value="Default">Choose Status</option>
            <option value="Full">Full Time</option>
            <option selected="selected" value="Part">Part Time</option>
            <option value="Not">No</option>
        <?php } elseif ($student == "Not") { ?>
            <option value="Default">Choose Status</option>
            <option value="Full">Full Time</option>
            <option value="Part">Part Time</option>
            <option selected="selected" value="Not">No</option>
        <?php } else { ?>
            <option value="Default">Choose Status</option>
            <option value="Full">Full Time</option>
            <option value="Part">Part Time</option>
            <option value="Not">No</option>
        <?php } ?>
        </select> <p id="valStatus"><?php if ($_SESSION['validArray'][3] == 1 && isset($_SESSION['validArray'][3])) {
            echo "Please select a valid option.";
}?></p>
            <br>
            <!-- buttons to go forward and backward -->
            <input type="submit" name="nextbutton" value="Next!"/>
            <input type="submit" name="btnBack" value="Previous"/>
    </form>

    <footer>
        <hr/>
        <p>Copyright Aaron Thawe</p>
    </footer>
<?php } ?>

<html>
<head>
    <title>First page</title>
</head>

<body>

</body>
</html>