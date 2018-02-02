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
session_start();//Send user to other pages if they should be
if (!($_SERVER['REQUEST_METHOD'] == 'POST')) {
    //check for back button
    if (($_SESSION['pagetracker'] < 3) || !(isset($_SESSION['pagetracker']))) {
        echo "<script>location.assign('./page2.php');</script>";
    } elseif ($_SESSION['pagetracker'] == 4) {
        echo "<script>location.assign('./thankpage.php');</script>";
    }
}
//test for POST access to decide whether to print form or first run validation
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    //test if going back
    if (((isset($_POST['btnBack'])) && ($_POST['btnBack'] == "Previous"))) {
        $_SESSION['pagetracker'] = 2;
        echo "<script>location.assign('./page2.php');</script>";
    }
    //boolean to confirm later if any validation issues found or not
    $_SESSION['page3valid'] = true;
    //run validation for all data
    foreach ($_SESSION['purchases'] as $purchase) {
        //test radio buttons
        if (!(isset($_POST[$purchase]))) {
            $_SESSION['page3valid'] = false;
            $_SESSION['validArray3'.$purchase][1] = 1;
        } else {
            $_SESSION['validArray3'.$purchase][1] = 0;
        }
        //test checkboxes
        if ($_POST[$purchase .'Recommend'] == "Default") {
            $_SESSION['page3valid'] = false;
            $_SESSION['validArray3'.$purchase][2] = 1;
        } else {
            $_SESSION['validArray3'.$purchase][2] = 0;
        }
        //save data if available
        if (isset($_POST[$purchase])) {
            $_SESSION[$purchase] = $_POST[$purchase];
        }
        if (isset($_POST[$purchase .'Recommend'])) {
            $_SESSION[$purchase .'Recommend'] = $_POST[$purchase .'Recommend'];
        }

    }
    //decide to move on or not
    if ($_SESSION['page3valid'] == true) {
        moveForwardPage3();
    } else {
        page3Form();
    }
} else {
    //only runs when page is being freshly opened
    foreach ($_SESSION['purchases'] as $purchase) {
        $_SESSION[$purchase] = "";
        $_SESSION[$purchase .'Recommend'] = "";
        $_SESSION['validArray3'.$purchase][1] = 0;
        $_SESSION['validArray3'.$purchase][2] = 0;
    }//output form
    page3Form();
}
//send user forward to thank you page
function moveForwardPage3()
{
    $_SESSION['pagetracker'] = 4;
    echo "<script>location.assign('./thankpage.php');</script>";
}
//print out form to user
function page3Form()
{
    ?>
    <header>
        <h1>Sony Satisfaction Survey: Page 3</h1>
        <hr/>
    </header>

    <p>For each product you've purchased, answer the questions below.</p>

    <form method="POST" action="./page3.php">
        <?php
        // loop for each purchase to send user form
        foreach ($_SESSION['purchases'] as $itemName) {
            itemSatisfaction($itemName);
        }
        ?>
        <!-- buttons for submission and moving back for user -->
        <input type="submit" name="nextbutton" value="Finish the survey!"/>
        <input type="button" name="btnBack" value="Previous"/>
    </form>
    <footer>
        <hr/>
        <p>Copyright Aaron Thawe</p>
    </footer>
<?php }
//outputs user satisfaction form for a single purchase, taking in the name of that purchase in $itemName parameter
function itemSatisfaction($itemName)
{
    ?>
    <p>How happy are you with this <?php echo $itemName?> on a scale from 1 (not satisfied) to 5 (very satisfied)?</p>
    <input type="radio" id="<?php echo $itemName?>option1"
    name="<?php echo $itemName?>[]" value="1">
    <label for="<?php echo $itemName?>option1">1</label>

    <input type="radio" id="<?php echo $itemName?>option2"
    name="<?php echo $itemName?>[]" value="2">
    <label for="<?php echo $itemName?>option2">2</label>

    <input type="radio" id="<?php echo $itemName?>option3"
    name="<?php echo $itemName?>[]" value="3">
    <label for="<?php echo $itemName?>option3">3</label>

    <input type="radio" id="<?php echo $itemName?>option4"
    name="<?php echo $itemName?>[]" value="4">
    <label for="<?php echo $itemName?>option4">4</label>

    <input type="radio" id="<?php echo $itemName?>option5"
    name="<?php echo $itemName?>[]" value="5">
    <label for="<?php echo $itemName?>option5">5</label>
    <!-- validation output for radio buttons -->
    <p id="valRadio<?php echo $itemName?>"><?php
    if ($_SESSION['validArray3'.$itemName][1] == 1 && isset($_SESSION['validArray3'.$itemName][1])) {
        echo "Please select one of these options.";
    }?></p>
    
    <p>Would you recommend the purchase of this <?php echo $itemName?> to a friend?</p>
    <select id ="<?php echo $itemName?>Recommend" name="<?php echo $itemName?>Recommend">
        <option value="Default"></option>
        <option value="Yes">Yes</option>
        <option value="No">No</option>
    </select>
    <!-- validation output for dropdown -->
    <p id="valSelect<?php echo $itemName?>"><?php
    if ($_SESSION['validArray3'.$itemName][2] == 1 && isset($_SESSION['validArray3'.$itemName][2])) {
        echo "Please select one of these options.";
    }?></p>

<?php }?>
<html>
<head>
    <title>Third Page</title>
</head>

<body>
    
</body>
</html>


