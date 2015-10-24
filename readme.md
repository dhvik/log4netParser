#Readme
log4netParser is a windows forms application that parses a log4net textfile and groups the data in to a more readable format.

##Usage
Will parse log4net files on a specific format and group by level and logger
Enter a log filename in the texbox and click parse to analyze the data.

Search for keywords in the message by entering it in the search box and clicking enter.

Double click on a grouped item to open a tab will all entries for that group.

Close opened tabs with the close (x) button

##Open with log4net parser context menu in Windows Explorer
To be able to use a context menu in the windows explorer you need to enter some information in the registry.
The [openWithLog4net.reg](log4netParser/openWithLog4net.reg) file contains an example on what information that
needs to be added in the registry. Edit this file and correct the path to the log4net.exe file and then double click it 
to add the information to the registry. It will be added as a shortcut for ALL files "*/shell".
