# Passion_Project
## Photo documentation
This application helps users document photo information right away, which includes film and lens type, ISO, aperture, shutter speed, focal length, photo title, description, the date when a photo is taken. Users also can add a type of film and lens.

Used: HTML, CSS, Bootstrap, JavaScript, C#, ASP.NET, Entity Framework

## Code reference
Professor Christine Bittle
 * varsity_mvp : https://github.com/christinebittle/varsity_mvp
 * BlogProject_7 : https://github.com/christinebittle/BlogProject_7

## Datatables
 * Film table: it contains filmID, brand name, film series, box speed.
 * Lens table: it contains lensID, brand name, lens information.
 * Photo table: it contains photoID, ISO, aperture, shutter speed, focal length, title, description, date, photo file, filmID and lensID as foreign keys.
 * The film table and lens table are parent table of photo table, which has one to many relationship.

## Features
 * This application has CRUD operations for each data table.
   * users can add new data.
   * users can see a list of films, lenses, photos.
   * users can update existing data.
   * users can delete existing data.
 * Extra features:
   * users can upload photo files
   * users can filter photos by film type.

## Challenges
This application was built with a code-first approach. So, one of my challenges was understanding the data relationships and how to use them in the code. For example, when I tried to retrieve the related data from another data table, I got confused about which has many and which has one in the relationship. Another challenge was debugging. Since I didn't get any error messages, I spent a few days solving the same problem. 

## How I solved the challenges
To understand the database structure better, I wrote it down to make it easier for me to understand data table relationships. Also, I checked the example database provided by the professor and other examples found by google search to compare with my data table relationships. 
I had a problem adding and updating data. It was working fine when I was adding and updating data from one table. However, after I included other tables' data, it didn't let me add or update data. There weren't any error messages, so I decided to check step by step to find out where the problem was, using a command line to test an action in the controller, debug.writeline and developer tool to see if data was passed correctly. Then, I found out that there was a problem in passing data from the form. After checking through the code, I found out that I was missing name attributes in all form elements.

## Another features I would like to add in the future
 * Add a many to many relationship such as photo categories.
   * Approach way: Create a category table which contains ID and category name. Add a categoryID to the photo table or create a bridge table that can hold photoID and categoryID.
 * If a photo file already exists, display the existing file in the photo update page. 
   * Approach way: In edit.cshtml, add a logic to check if a file exists and get the file data associated with a photo ID.
 * Display photos in the effective way.
   * Approach way: I would like to display a photo in a full screen when users click the photo in details.cshtml. To do that, I will find a built library or make it manually with CSS and JavaScript.
