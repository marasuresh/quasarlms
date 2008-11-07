// *********************************** //  
// Listado genérico //

function Listado()
{
    this.indice = 0;
    this.items = new Array();
}

Listado.prototype.add = function(item)
{
    this.items.push(item);
}

Listado.prototype.read = function()
{
    return this.items[this.indice];
}

Listado.prototype.next = function()
{
    if ((this.indice + 1) < this.length())
    {
        this.indice++;
        return true;
    }
    else
        return false;
}

Listado.prototype.prev = function()
{
    if ((this.hasItems) && (this.indice > 0))
    {
        this.indice--;
        return true;
    }
    else
        return false;
}

Listado.prototype.moveFirst = function()
{
    this.indice = 0;
}

Listado.prototype.moveLast = function()
{
    if (this.hasItems)
        this.indice = this.items.length() - 1;
}


Listado.prototype.hasItems = function()
{
    return (this.items.length > 0);
}

Listado.prototype.length = function()
{
    return this.items.length;
}

