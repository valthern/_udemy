# Programa de reserva de Hoteles

cliente = "Emilio Freixas"
dias_estancia = 12
vista_mar = True
# La tarifa varía si es una habitación con vista al mar o no
tarifa_diaria = 1500.0 if vista_mar else 1200.0

print("*** Sistema de Reserva de Hoteles ***")

print("Nombre del Cliente:", cliente)
print("Días de estancia:", dias_estancia)
print("Tarifa diaria:", tarifa_diaria)
print("Tarifa total:", tarifa_diaria * dias_estancia)
print("Tiene vista al mar:", vista_mar)

dias_estancia = 14
# Si la estancia es mayor o igual a 15 días, entonces se aplica un descuento del 10%
descuento = 0.9 if dias_estancia >= 15 else 1.0

print()
print("Nombre del Cliente:", cliente)
print("Días de estancia:", dias_estancia)
print("Tarifa diaria:", tarifa_diaria * descuento)
print("Tarifa total:", (tarifa_diaria * dias_estancia) * descuento)
print("Tiene vista al mar:", vista_mar)
