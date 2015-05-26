# Classes #

  * Game : The highest main class.
    * Log : The move log that is shared through all components of the game
    * View : The graphics and such.
      * Screen : The window or game board or text buffer depending on the interface
      * Sprites : The representation of objects, mapped to the names.
    * Model : The data structures
      * Board : Defaults to an 8 by 8 square, checkered chess board.
        * Team : A group of Pieces
      * Piece : The very basic structure of a game piece (i.e. character)
        * HP
        * Ability List
        * Name
    * Controller : Handles user input
      * Gamepad : Handles input from game pad for Xbox or PC
      * Type Click : Handles input from mouse and keyboard
      * Text : Handles basic text input.
        * Telcom : Handles multi-player through internet connection.
        * AI : Artificial intelligence is pure text based and an extension of a Text controller.