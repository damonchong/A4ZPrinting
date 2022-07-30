# A4ZPrinting
Approximating for Zebra printing (i.e. A4ZPrinting) is a Zebra library to design, generate ZPL commands and print labels for programmers.

## What is it?
A4ZPrinting is a object oriented (OO) library that uses and extends SharpZebra (https://github.com/rkone/sharpzebra) to generate ZPL commands for Zebra printers.

## Why A4ZPrinting?
I got tired of trying to piece low level ZPL commands together to get a successful label printed. There were too many elements to juggle like size of the labels, printer's DPI, exact x and y coordinates of the text and barcodes. On top of these, trying to ensure that the text and barcodes don't exceed the physical label dimensions and don't intersect one another.

## How does it do it?
First, it abstracts out the physical objects like your printer model and print labels. Then, it uses the Composite design pattern to create derived classes like text fields and barcodes (known as field classes). The field classes are placed inside a template class. The template class are applied onto a label class. And finally the label class are send to the printer class to generate the final ZPL commands.

## Who to approach?
This is a work-in-progress so if you encounter any issue or have a question regarding this library, you can drop me an email at c_h_o_n_g_d_a_m_o_n@g-m-a-i-l.c0m (remove the underscores and figure out the domain) although no guarantee I will respond fast.

## Where to start?
Please goto the [wiki page](https://github.com/damonchong/A4ZPrinting/wiki/Getting-Started) for detailed instructions on how to test out and customized the library to suit the zebra printer model, label types and layout you have in mind.

## Caveat emptor!
This library approximates the layout and design you have in mind, generating the ZPL commands and sending them to be printed onto labels. But like all approximations, it's not perfect. In the event that the layout and design is not exact or as accurate, at least, the generated ZPL commands give you something to work with instead of starting from zero. Tweak the generated ZPL commands to achieve your desired results. 
