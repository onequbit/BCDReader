clear
# IMPORTANT: bcdedit /enum requires an ELEVATED session.
$bcdOutput = (bcdedit /enum) -join "`n" # collect bcdedit's output as a *single* string

# Initialize the output list.
$entries = New-Object System.Collections.Generic.List[pscustomobject]]
# $bcdinfo = bcdedit.exe

#$bcdinfo
$paritions = []
for ($i=0; $i -le $bcdinfo.length; $i++) 
{
    $line = @($bcdinfo)[$i]
    
    if ($line -like "Windows Boot*")
    {
        $line += "***"
    }
    echo $line.length
}

# $test = ConvertTo-Json -InputObject $bcdinfo
# $test


