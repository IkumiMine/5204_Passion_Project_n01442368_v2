# 5204_Passion_Project_n01442368_v2
Passion Project: Photo documentating application

## Code reference
Professor Christine Bittle
 - varsity_mvp : https://github.com/christinebittle/varsity_mvp
 - BlogProject_7 : https://github.com/christinebittle/BlogProject_7

## Application Description
 - This application will help users to document photo information right away, which includes film and lens type, ISO, aperture, shutter speed, focal length, photo title, description, the date when a photo is taken.

## Datatables
 - Film table: it contains filmID, brand name, film series, box speed.
 - Lens table: it contains lensID, brand name, lens information.
 - Photo table: it contains photoID, ISO, aperture, shutter speed, focal length, title, description, date, photo file, filmID and lensID as foreign keys.
 - The film table and lens table are parent table of photo table, which has one to many relationship.

## Features
 - This application has CRUD operations for each data table.
   - users can add new data.
   - users can see a list of films, lenses, photos.
   - users can update existing data.
   - users can delete existing data.
 - Extra features:
   - users can upload photo files
   - users can filter photos by film type.

## Another features I would like to add in the future
 - Add a many to many relationship such as photo categories.
   - Approach way: Create a category table which contains ID and category name. Add a categoryID to the photo table or create a bridge table that can hold photoID and categoryID.
 - If a photo file already exists, display the existing file in the photo update page. 
   - Approach way: In edit.cshtml, add a logic to check if a file exists and get the file data associated with a photo ID.
 - Display photos in the effective way.
   - Approach way: I would like to display a photo in a full screen when users click the photo in details.cshtml. To do that, I will find a built library or make it manually with CSS and JavaScript.
