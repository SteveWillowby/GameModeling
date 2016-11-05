main: *.cs
	mcs -out:bin/Main.exe *.cs

objtest: testing/*.cs old/*.cs
	mcs -out:bin/ObjTest.exe testing/*.cs old/*.cs

parse: parsing/*.cs
	mcs -out:bin/Parser.exe parsing/*.cs
