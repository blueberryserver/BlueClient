REM for %%f in (*.proto) do for /F "tokens=1 delims=1." %%p in ("%%f") do  .\protoc.3.0.0 --cpp_out=..\Cpp\ %%p.proto
REM for %%f in (*.proto) do for /F "tokens=1 delims=1." %%p in ("%%f") do  .\csharpbin\protogen -i:%%p.proto -o:.\%%p.cs
for %%f in (*.proto) do for /F "tokens=1 delims=1." %%p in ("%%f") do  .\protoc.3.0.0 --python_out=.\ %%p.proto

