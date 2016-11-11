main: Main.cs instantiations/*.cs
	mcs -out:bin/Main.exe Main.cs instantiations/*.cs

objtest: testing/*.cs old/*.cs
	mcs -out:bin/ObjTest.exe testing/*.cs old/*.cs

parse: parsing/*.cs instantiations/*.cs
	mcs -out:bin/Parser.exe parsing/*.cs instantiations/*.cs
