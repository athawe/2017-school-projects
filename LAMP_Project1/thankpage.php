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
if (($_SESSION['pagetracker'] < 4) || !(isset($_SESSION['pagetracker']))) {
    echo "<script> location.replace(\"./page3.php\"); </script>";
}?>
<html>
<head>
    <title>Thank you Page</title>
</head>

<body>
    <header>
        <h1>Sony Satisfaction Survey: Thank you!</h1>
        <hr/>
    </header>
    <h2>Thank you for completing our survey!</h2>
    <!-- table for output -->
    <table>
        <tr>
            <td>Your Name: </td>
            <td><?php echo $_SESSION['fullName'] ?></td>
        </tr>
        <tr>
            <td>Age: </td>
            <td><?php echo $_SESSION['age'] ?></td>
        </tr>
        <tr>
            <td>Student Status: </td>
            <td><?php echo $_SESSION['student'] ?></td>
        </tr>
        <tr>
            <td>How you purchased your product: </td>
            <td><?php echo $_SESSION['howPurchased'] ?></td>
        </tr>
        <tr>
            <!-- loop through purchases and output each one, formatting the last with a period -->
            <td>Products Purchased: </td>
            <td><?php $lastElement = end($_SESSION['purchases']);
            foreach ($_SESSION['purchases'] as $item) {
                if ($item == $lastElement) {
                    echo $item.".";
                } else {
                    echo $item.", ";
                }
            } ?></td>
        </tr>       
        <?php
        // loop through purchases and output rating and recommendations for each one in it's own row
        foreach ($_SESSION['purchases'] as $itemName) {
            outputSatisfaction($itemName);
        }
        ?>

    </table>
    <footer>
        <hr/>
        <p>Copyright Aaron Thawe</p>
    </footer>
    <?php
    //save data to MySQL table named surveyData
    save_data();
    //destroy session
    session_destroy();?>  
</body>
</html>
<!-- output user input for a certain purchase based on given name in $itemName parameter -->
<?php function outputSatisfaction($itemName)
{
    ?>
<tr>
    <td>Satisfaction for <?php echo $itemName ?>:</td>
    <td><?php $val = $_SESSION[$itemName][0];
    echo $val;?></td>
</tr>
<tr>
    <td>Would you recommmend this product:</td>
    <td><?php echo $_SESSION[$itemName."Recommend"];?></td>
</tr>
<?php }
// save_data function establishes connection, generates query for MySQL server, completes query, and closes connection
function save_data()
{
    $db_conn = new mysqli('localhost', 'lamp1user', '!Lamp1!', 'demo');
    if ($db_conn->connect_errno) {
        die("Could not connect to database server".$db_host."\n Error: ".$db_conn->connect_errno ."\n Report: ".$db_conn->connect_error."\n");
    }
    //set up variables
    $name = $db_conn->real_escape_string($_SESSION['fullName']);
    $age = $db_conn->real_escape_string($_SESSION['age']);
    $student = $db_conn->real_escape_string($_SESSION['student']);
    $howPurchased = $db_conn->real_escape_string($_SESSION['howPurchased']);
    $count = 1;
    // set up strings
    $qry1 = "INSERT INTO surveyData (name, age, status, howPurchased";
    $qry2 = ") values('".$name."', '".$age."', '".$student."', '".$howPurchased;
    foreach ($_SESSION['purchases'] as $itemName) {
        ${'purchase'.$count} = $itemName;
        ${'satisfaction'.$count} = $_SESSION[$itemName][0];
        ${'recommend'.$count} = $_SESSION[$itemName."Recommend"];
        $qry1 .= ", purchase".$count;
        $qry1 .= ", satisfaction".$count;
        $qry1 .= ", recommend".$count;
        $qry2 .= "', '".${'purchase'.$count};
        $qry2 .= "', '".${'satisfaction'.$count};
        $qry2 .= "', '".${'recommend'.$count};
        $count++;
    }
    // concat strings
    $qry = $qry1.$qry2."')";
    $db_conn->query($qry); //run query
    $db_conn->close(); //close connection
}?>