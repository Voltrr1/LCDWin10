# LCDWin10
Alphanumeric LCD (16x2 and other) for Windows 10 IoT Core

More details can be found on Hacker.Io

Library
Library is based on standard Arduino (thanks guys!) library. Current version supports only 4 wire connections (8 wire not tested now so not released). 

Initialization
Sample code is here :

using Win10_LCD;

// cols, rows according to your LCD

var lcd = new LCD(20, 4); 

//pin numbers connected to RS, E, D4, D5, D6 and D7. This is for sample schematic

//initialization take some time so it is async call

await lcd.InitAsync(18, 23, 24, 5, 6, 13);

//let's clear screen, also async

await lcd.clearAsync();

Notes : clearAsync() clears screen, clear buffers and also set cursor to home position. Can be used for both modes (see below)

Library can work in two modes of usage :

Text output mode
This is basic and very simple mode of use. All what you need to do is :

- Connect LCD

- Initialize LCD

- Print your string to LCD

Library will take care of rest - will cut your string to proper size (and will scroll it in future releases), scroll strings if you send more strings than you have lines, take care of cursors and positions. Well, you can replace your Console.WriteLine with LCD.WriteLine and you are done :-D

Code looks like :

lcd.WriteLine("Console output 1");
lcd.WriteLine("Console output 2");
lcd.WriteLine("Console output 3");
lcd.WriteLine("Console output 4");
//no problem, autoscroll is supported !!
lcd.WriteLine("Console output 5");
//let's clear it and start again from top
await lcd.clearAsync();
lcd.WriteLine("LCD Rulez :-D");



Direct mode
This is same mode as Arduino library support. You can control text, cursor position, upload special characters and do rest of stuff. Usage is little bit complicated but you have higher level of freedom.



await lcd.clearAsync();

//cursor is on home - 0x0

lcd.write("Windows 10 IoT Core ");

//second line

lcd.setCursor(0, 1);

lcd.write("IP:"+ CurrentIPAddress());

lcd.setCursor(0, 2);

lcd.write("Name:"+ CurrentComputerName());

while (true)

{

//back to line 3 so text will override itself

lcd.setCursor(0, 3);

lcd.write("Time :"+DateTime.Now.ToString("HH:mm:ss.ffff"));

}



Direct mode have one feature which can be unwanted - lines can overload to different one but chip have strange memory layout and so you can see text from first line on 3rd line of 4 line LCD or from 3rd line will text overlap to number 2 and so. Please take care of cutting strings for proper ranges. Text output mode do it for you, direct mode is free and so you have to do it manually.



Warning - Modes are not compatible. Please don't use them at once without deeper knowledge of library internals. Chose which way you want to go and stay on road.