erase %1\FATBox.Initialization\Resources\FATBox.Lua.zip
if %errorlevel% neq 0 (
	exit /b %errorlevel%
)
"C:\Program Files (x86)\7-Zip\7z.exe" a -r %1\FATBox.Initialization\Resources\FATBox.Lua.zip %1"\FATBox.Lua\contents\*.*"
if %errorlevel% neq 0 (
	exit /b %errorlevel%
)