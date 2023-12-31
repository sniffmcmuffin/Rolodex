# Rolodex
#
# I had to create a whole new project 'KonsolApp' 
# to be able to use /Shared project. 
#
# Known bugs:
# ContactAddviewModel.cs
# - NavigateToList() - Does not update the list with the new contact. 
#                      Requires restarting the app. Why?
#
# TODO:
# - Add() in ContactEdit/AddViewModel.cs need to be cleaned up and seperate concerns.
# - Add more summary commenting through the solution. 
# - Make more tests. Decide which tests go to Konsol - Wpf - Shared. 
# - Make it so a new contact takes first available free id in the list in WfpApp.
# - Change to an actual update of the contact in KonsolApp.