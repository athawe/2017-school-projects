<?php
/* submit zip & sql source file
5 pages

index
     intro
     initialize SESSION
        have array of pages visited
        keep track of last page user is on
        have array of pages finished
     begin button
        move to page 1, set page to finished
Page 1
    Don't let user in unless previous page is completed
    Form for questions
    Next Button
        input validation using PHP
        keep user on same page if validation fails
        repopulate all text fields
        repopulate chkbox, select & radio buttons
        Bonus for saving data to MySQL database
    Previous Button
Page 2
    Same as page 1
Page 3
    Generated based off of purchases from page 2
Thank you page
    show answers



PLAN
set up appearance
set up basic data storage and functionality
add in validation
add in correct page access
*/
?>