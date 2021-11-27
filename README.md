# Kamihime-ScenePlayer

The game resource file name is encrypted by blowfish, showing that the file name is very messy. This program decrypts the file path with the key, so that the resource path of CG can be obtained through the character ID, and then extract resources.  
And, this repo also attach with an ero scene viewer. If you have extracted game resources, you can use this viewer to view ero scene scenes without logging in to the game.

# Usage

## Base Usage
Unzip directly and open main.html in the HTML folder  
If you want to view ero scene, go to kamihime ero scene folder in asset folder.  

- Character ID with less than four digits should be filled with 0 in the front.   
- For example, the ID you want to fill in is 19. You can't write 19 directly, write 0019. 

The following describes the function usage here by taking the character 紅醇麗姫- ティシュトリヤ as an example. 
### IF YOU WANT TO GET A STANDING-PICTURE
- Find Her Character ID and confirm Her Type     
- Her character ID is 5163. Select her category in the selection. She belongs to Kamihime, select the category related to Kamihime, and fill in 5163 in the text box and click Submit.   
### IF YOU WANT TO GET AN ERO-SCENE-PICTURE
- For example, to view the H scene thumbnail of 紅醇麗姫 - テシめトリヤ, you need to fill in 5163-3-2, "3" is the scene ID, you can fill it with '2' too.
- However, it should be noted that if you want to view the ero scene thumbnail, you must bring the scene ID and the fixed character "-2"(Must be placed at the end).   
- Example : 5163-3-2,5114-2-2,5099-3-2 (Format: CharacterID-SceneID-2)  
## Get Scenarios Json
According to the above usage rules, you can do things below in order.
- fill the ID in the second text box in the order of this HTML file, and click Submit button, you will get a link to scenarios json and a folder name.  
- if you want to view ero scene in the attached ero scene viewer, Fill the folder name into the text box of the viewer and click download button to view ero scene.  

# Resource Save Method of Kamihime
``` js
// part of Blowfish.js
crypto.cipherModes = {
	// summary:
	//		Enumeration for various cipher modes.
	ECB:0, CBC:1, PCBC:2, CFB:3, OFB:4, CTR:5
};
crypto.outputTypes = {
	// summary:
	//		Enumeration for input and output encodings.
	Base64:0, Hex:1, String:2, Raw:3
};

// Encrypt with key bLoWfIsH (Method blowfish)
var FinalText = blowfish.encrypt(type+x+p,"bLoWfIsH",{
            outputType: 1,
            cipherMode: 0
})

// Cut FinalText as the folder name 
function getPath(t) {
        var r = t.lastIndexOf(".")
          , e = -1 < r ? t.substr(0, r) : t;
        return [[-6, 3], [-3, 3]].map(function(t) {
            return String.prototype.substr.apply(e, t)
        }).join("/") + "/" + t;
}

```

# Ref
Blowfish.js from Dojo Toolkit 1.8.1  

