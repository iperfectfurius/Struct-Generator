# What is Struct-Generator?

It's an app that create templates based on actual directories and their files of the target folder for duplicate at any moment at any other directory.

**Target folder** is the directory at you opened struct-generator.

The **purpose** of this application is to create a common structure that you normally need to replicate all the time for new projects and then automate it with just one command.
You can make your own templates if you don't have a actual structure.

Struct Generator **don't write binary or executable files**.

## Installation

#### Recomended

Download the latest version of [Struct-Generator](https://github.com/iperfectfurius/Struct-Generator/releases).
Save the file wherever you want or put in Windows directory to avoid this step , then execute with elevated permissions(only first time) and write ***setpath*** command and enter. [Example](https://i.ibb.co/1LSMFgR/ezgif-com-gif-maker-1.gif)

Now you can open a cmd/powershell and type ***struct_generator*** followed by argumets if you want. Remember **Target folder** is the directory at you opened or execute struct-generator. [Example](https://i.ibb.co/TB94Cvp/ezgif-com-gif-maker-2.gif)



## Arguments

* **-a [name_of_template]**: Create a structure based on templates saved(templates are saved on applicationData folder, %appdata%/Struct_Generator/templates).
* **-c [name of template]**: Create a template based on target directory(Console current directory) that contains all root, files and content from files.
* **templates**: Show all templates saved.

## In-app commands

* **help**: List of commands.
* **help -n**: Example of new template.
* **templates**: Show all templates saved.
* **templates -c**: Create a template based on target directory(Console current directory) that contains all root, files and content from files. 
* **templates -a [name_of_template]**: Create a structure based on templates saved(templates are saved on applicationData folder, %appdata%/Struct_Generator/templates).
* **templates [name_of_template]**: Show the selected template if exist.
* **-n**: Create a default template and opened with default text editor.
* **-rm [name_of_template]**: Delete a template.
* **setpath**: Add Struct Generator to evironment variables.
