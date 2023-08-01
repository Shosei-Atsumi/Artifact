#----------------------------------------------------------------
# Generated CMake target import file for configuration "Release".
#----------------------------------------------------------------

# Commands may need to know the format version.
set(CMAKE_IMPORT_FILE_VERSION 1)

# Import target "Tesseract::libtesseract" for configuration "Release"
set_property(TARGET Tesseract::libtesseract APPEND PROPERTY IMPORTED_CONFIGURATIONS RELEASE)
set_target_properties(Tesseract::libtesseract PROPERTIES
  IMPORTED_IMPLIB_RELEASE "${_IMPORT_PREFIX}/lib/tesseract50.lib"
  IMPORTED_LOCATION_RELEASE "${_IMPORT_PREFIX}/bin/tesseract50.dll"
  )

list(APPEND _IMPORT_CHECK_TARGETS Tesseract::libtesseract )
list(APPEND _IMPORT_CHECK_FILES_FOR_Tesseract::libtesseract "${_IMPORT_PREFIX}/lib/tesseract50.lib" "${_IMPORT_PREFIX}/bin/tesseract50.dll" )

# Commands beyond this point should not need to know the version.
set(CMAKE_IMPORT_FILE_VERSION)
