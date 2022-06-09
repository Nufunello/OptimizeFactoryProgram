import math
from scipy.optimize import linprog

class Resource:
	def __init__(self, name, cost, count):
		self.name = name
		self.cost = cost
		self.count = count

class Constraint:
	def __init__(self, resource):
		self.max_count = resource.count
		self.min_count = 0

	def setMinCount(self, min_count):
		self.min_count = min_count
		return self

	def setMaxCount(self, max_count):
		self.max_count = max_count
		return self

class Ingredient:
	def __init__(self, resource, count, constraint):
		self.resource   = resource
		self.count      = count
		self.constraint = constraint

class Receipt:
	def __init__(self, product, cost):
		self.ingredients = []
		self.product   = product
		self.cost      = cost

	def addIngredient(self, resource, count, constraint = None):
		constraint = constraint or Constraint(resource)
		self.ingredients.append(Ingredient(resource, count, constraint))
		return self
		
	def count(self, resource):
		for ingredient in self.ingredients:
			if (id(ingredient.resource) == id(resource)):
				return ingredient.count
		return 0

class Product:
	def __init__(self, name, cost):
		self.name = name
		self.cost = cost

import sys
resources = map(lambda resource: 
	Resource(
		resource.split(',')[0], 
		resource.split(',')[1], 
		resource.split(',')[2]
	), 
	sys.argv[1].split(';')
)
resources = list(resources)

products = map(lambda resource:
	Product(
		resource.split(',')[0], 
		float(resource.split(',')[1])
	), 
	sys.argv[2].split(';')
)
products = list(products)

receipts = []
for inrec in sys.argv[3].split(';'):
	prod = next(x for x in products if x.name == inrec.split(',')[0])
	receipt = Receipt(prod, prod.cost)

	for ingridient in inrec.split(',')[1].split('|'):
		material = next(x for x in resources if x.name == ingridient.split('_')[0])
		count = float(ingridient.split('_')[1])
		receipt.addIngredient(material, count)

	receipts.append(receipt)



obj = list(map(lambda receipt: receipt.cost, receipts))
rhs_ineq = list(map(lambda resource: resource.count, resources))
lhs_ineq = list(map(lambda _: list(map(lambda _: 0, receipts)), resources))
for i in range(len(resources)):
	lhs_ineq[i] = list(map(lambda receipt: receipt.count(resources[i]), receipts))
obj = list(map(lambda element: -element, obj))
bnd = [(0, float("inf"))] #x >= 0
opt = linprog(c=obj, A_ub=lhs_ineq, b_ub=rhs_ineq, bounds=bnd)

print(list(opt.x))
print(list(map(lambda receipt: receipt.product.name, receipts)))
