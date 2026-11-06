local cadena = "Hola Mundo"

-- forma 1
local longitud1 = cadena:len()
-- forma 2
local longitud2 = #cadena
-- forma 3
local longitud3 = string.len(cadena)

print("La longitud de la cadena \"" .. cadena .. "\" es: " .. longitud1)
print("La longitud de la cadena \"" .. cadena .. "\" es: " .. longitud2)
print("La longitud de la cadena \"" .. cadena .. "\" es: " .. longitud3)
