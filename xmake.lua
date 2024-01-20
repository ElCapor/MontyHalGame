add_rules("mode.debug", "mode.release")
set_allowedmodes("debug", "release")
set_defaultmode("debug")

set_encodings("utf-8")
set_languages("c++20")
set_warnings("allextra")
add_requires("raylib", "raygui")
target("MontyHal") do 
    set_kind("binary")
    add_packages("raylib", "raygui")

    add_includedirs("include")
    add_files("src/**.cpp")
    add_headerfiles("include/**.hpp")


end