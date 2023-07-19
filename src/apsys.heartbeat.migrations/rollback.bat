echo off
cd bin/Debug/net6.0
template.migrations.exe /cnn:"Server=.;Database=databasename.devel;Trusted_Connection=True;"  /rollback:"one"
cd../../..
echo on
pause