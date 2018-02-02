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
session_start();
//initialize pagetracker if this is a new user
if (!(isset($_SESSION['pagetracker']))) {
    $_SESSION['pagetracker'] = 0;
} //Send user to most recent page, or next page if they should be sent there
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $_SESSION['pagetracker'] = 1;
    echo "<script>location.assign('./page1.php');</script>";
} elseif ($_SESSION['pagetracker'] == 1) {
    echo "<script>location.assign('./page1.php');</script>";
} elseif ($_SESSION['pagetracker'] == 2) {
    echo "<script>location.assign('./page2.php');</script>";
} elseif ($_SESSION['pagetracker'] == 3) {
    echo "<script>location.assign('./page3.php');</script>";
} elseif ($_SESSION['pagetracker'] == 4) {
    echo "<script>location.assign('./thankpage.php');</script>";
}
?> <!--html output -->
<html>
<head>
    <title>Electronics Manufacturer Survey2</title>
</head>

<body>
    <header>
        <h1>Sony Satisfaction Survey</h1>
        <hr/>
    </header>

    <p>Welcome to our customer satisfaction survey. This survey will take up to 10 minutes to complete. All you need to 
    do is follow the buttons at the bottom of each page, filling all relevant questions. A few notes:</p>
    
    <ul>
        <li>If your answers aren't valid, you'll be unable to move forward without changing them.</li>
        <li>If you leave your survey partway through, you'll be returned to the page you left on.</li>
        <li>You must complete each page in order and use the buttons to move forward and backwards.</li>
    </ul>
    <form method="POST"> <!--submit button to send user to next page -->
        <input type="submit" name="beginbutton" value="Begin"/>
    </form>
    <footer>
        <hr/><p>Copyright Aaron Thawe</p>
    </footer>
</body>
</html>