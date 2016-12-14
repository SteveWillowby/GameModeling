//////////////////////////////////////////////////////////////////////////////
//                                                                          //
//                                  Intro                                   //
//                                                                          //
//////////////////////////////////////////////////////////////////////////////

This code is intended to make a program capable of reading in text files 
describing a game, creating an instance of that game, and presenting an
interface through which the game can be played.

It will make a ton more sense if you've read documentation of the model.

//////////////////////////////////////////////////////////////////////////////
//                                                                          //
//                            Software Stucture                             //
//                                                                          //
//////////////////////////////////////////////////////////////////////////////

The files are split into two categories, "instantiations" and "parsing."

The code under instantiations is used to actually represent board game data and
functionality. The code under parsing is used to read in files and set up the
initial game instance.

Some classes recursively reference each other. For instance, ObjectSet stores
Objects, and an Object has an ObjectSet called _contains.

********** Fully coded classes **********

//Instantiations
Action
Effect
Object
ObjectSet
Player
PlayerState
Requirement
Timer

//Parsing
Chunk
Chunks
File
Line
RequirementParser

********** Partially coded classes **********

//Instantiations
GameState

//Parsing
EffectParser

********** Uncoded functionality **********

Reading instances file
User interface
Searching for which actions can be selected, and on which objects
Win conditions
Putting the parsing pieces together in a function/class that calls them all
