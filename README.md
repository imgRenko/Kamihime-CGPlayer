# Kamihime-GameResource-Calculator

The game resource file name is encrypted by AES, showing that the file name is very messy. This program decrypts the file path with the key, so that the resource path of CG can be obtained through the character ID, and then extract resources.  
And, this repo also attach with an H scene viewer. If you have extracted game resources, you can use this viewer to view h scene scenes without logging in to the game.

# Usage
Character ID with less than four digits should be filled with 0 in the front.   
For example, the ID you want to fill in is 19. You can't write 19 directly, write 0019. 
The following describes the function usage here by taking the character 紅醇麗姫- ティシュトリヤ as an example. Her character ID is 5163. Select her category in the selection. She belongs to Kamihime, select the category related to Kamihime, and fill in 5163 in the text box and click Submit.   
However, it should be noted that if you want to view the H scene thumbnail, you must bring the scene ID and the fixed character "-2"(Must be placed at the end).   
For example, to view the H scene thumbnail of 紅醇麗姫 - テシめトリヤ, you need to fill in 5163-3-2, "3" is the scene ID, you can fill it with 2 too.
