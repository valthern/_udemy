-- Los índices en Lua comienzan desde 1, no desde 0 como en otros lenguajes de programación.
local cadena = "Hola, mundo! Bienvenidos a Lua."
local indice = cadena:find("o")
print("Indice donde se encuentra la primera 'o': " .. indice)

indice = cadena:find("o", indice + 1)
print("Indice donde se encuentra la segunda 'o': " .. indice)

indice = cadena:find("z")
-- Esto imprimirá 'nil' ya que 'z' no está en la cadena.
print("Indice donde se encuentra la letra 'z': " .. tostring(indice))