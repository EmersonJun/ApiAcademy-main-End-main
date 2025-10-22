const API_URL = '/api/equipamentos';

async function carregarEquipamentos() {
  const res = await fetch(API_URL);
  const data = await res.json();
  const tbody = document.querySelector('#tabela tbody');
  tbody.innerHTML = '';
  data.forEach(eq => {
    const tr = document.createElement('tr');
    tr.innerHTML = `
      <td>${eq.nome}</td>
      <td>${eq.tipo}</td>
      <td>${eq.status}</td>
      <td>
        <button onclick="removerEquipamento(${eq.id})">Remover</button>
      </td>`;
    tbody.appendChild(tr);
  });
}

async function removerEquipamento(id) {
  if (!confirm('Deseja remover este equipamento?')) return;
  await fetch(`${API_URL}/${id}`, { method: 'DELETE' });
  carregarEquipamentos();
}

async function cadastrarEquipamento(equipamento) {
  await fetch(API_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(equipamento)
  });
  alert('Equipamento cadastrado!');
  window.location.href = '/EquipamentosView/Index';

}

async function gerarRelatorio() {
  const res = await fetch(API_URL);
  const equipamentos = await res.json();
  const tipos = {};
  let totalValor = 0;

  equipamentos.forEach(eq => {
    tipos[eq.tipo] = (tipos[eq.tipo] || 0) + 1;
    totalValor += eq.valor;
  });

  let html = '<h2>Quantidade por Tipo</h2><ul>';
  for (const tipo in tipos) {
    html += `<li>${tipo}: ${tipos[tipo]}</li>`;
  }
  html += `</ul><h3>Valor total dos equipamentos: R$ ${totalValor.toFixed(2)}</h3>`;
  document.getElementById('relatorio').innerHTML = html;
}
